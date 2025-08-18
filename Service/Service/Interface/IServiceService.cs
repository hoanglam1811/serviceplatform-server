using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;


namespace Service.Service.Interface
{
	public interface IServiceService
		: IGenericService<Services, CreateServiceDTO, UpdateServiceDTO, ServiceDTO>
	{
		Task<IEnumerable<ServiceDTO>> GetServicesByUserIdAsync(Guid userId);
		Task<IEnumerable<ServiceDTO>> GetServicesByCategoryIdAsync(Guid categoryId);
	}
}
