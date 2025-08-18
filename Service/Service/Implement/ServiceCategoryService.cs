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
	public class ServiceCategoryService
		: GenericService<ServiceCategory, ServiceCategoryDTO, CreateServiceCategoryDTO, UpdateServiceCategoryDTO>, IServiceCategoryService
	{
		public ServiceCategoryService(IGenericRepository<ServiceCategory> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<ServiceCategoryDTO?> GetByNameAsync(string name)
		{
			var categories = await _genericRepository.GetAllAsync();
			var category = categories.FirstOrDefault(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			return _mapper.Map<ServiceCategoryDTO?>(category);
		}
	}
}
