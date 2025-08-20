using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface IServiceCategoryService
		: IGenericService<ServiceCategory, CreateServiceCategoryDTO, UpdateServiceCategoryDTO, ServiceCategoryDTO>
	{
		Task<ServiceCategoryDTO?> GetByNameAsync(string name);
	}
}
