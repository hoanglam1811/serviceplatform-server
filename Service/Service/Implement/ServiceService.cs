using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DTO;
using Repository.Entities;
using Repository.Repository.Interface;
using Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Implement
{
	public class ServiceService
		: GenericService<Services, CreateServiceDTO, UpdateServiceDTO, ServiceDTO>, IServiceService
	{
		public ServiceService(IGenericRepository<Services> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<IEnumerable<ServiceDTO>> GetServicesByUserIdAsync(Guid userId)
		{
			var services = await _genericRepository.GetAllAsync(q => q.Include(s => s.Category));
			var result = services.Where(s => s.UserId == userId);
			return _mapper.Map<IEnumerable<ServiceDTO>>(result);
		}

		public async Task<IEnumerable<ServiceDTO>> GetServicesByCategoryIdAsync(Guid categoryId)
		{
			var services = await _genericRepository.GetAllAsync();
			var result = services.Where(s => s.CategoryId == categoryId);
			return _mapper.Map<IEnumerable<ServiceDTO>>(result);
		}

    public async Task<ServiceDTO> UpdateNoForeignId(UpdateServiceDTO dto)
    {
      var service = await _genericRepository.GetByIdAsync(dto.Id);
      var entity = _mapper.Map<Services>(dto);
      entity.UserId = service.UserId;
      entity.Status = "Active";
      var result = await _genericRepository.UpdateAsync(entity);
      return _mapper.Map<ServiceDTO>(result);
    }
  }
}
