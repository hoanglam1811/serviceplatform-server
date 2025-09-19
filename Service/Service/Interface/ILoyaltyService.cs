using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface ILoyaltyService
		: IGenericService<LoyaltyPoint, CreateLoyaltyPointDTO, UpdateLoyaltyPointDTO, LoyaltyPointDTO>
	{
		Task<int?> GetPointsByUserIdAsync(Guid userId);
		Task<bool> AddPointsAsync(Guid userId, int points);
		Task<bool> RedeemPointsAsync(Guid userId, int points);
	}
}
