using AutoMapper;
using Repository.DTO;
using Repository.Entities;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Service.Interface;

namespace Service.Service.Implement
{
	public class ProviderProfileService
		: GenericService<ProviderProfile, CreateProviderProfileDTO, UpdateProviderProfileDTO, ProviderProfileDTO>, IProviderProfileService
	{
		public ProviderProfileService(IGenericRepository<ProviderProfile> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		// Ví dụ: tìm hồ sơ theo UserId
		public async Task<ProviderProfileDTO?> GetByUserIdAsync(Guid userId)
		{
			var providers = await _genericRepository.GetAllAsync();
			var provider = providers.FirstOrDefault(p => p.UserId == userId);
			return _mapper.Map<ProviderProfileDTO?>(provider);
		}
	}
}
