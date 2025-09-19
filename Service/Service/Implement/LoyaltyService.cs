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
	public class LoyaltyService
		: GenericService<LoyaltyPoint, CreateLoyaltyPointDTO, UpdateLoyaltyPointDTO, LoyaltyPointDTO>, ILoyaltyService
	{
		public LoyaltyService(IGenericRepository<LoyaltyPoint> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<int?> GetPointsByUserIdAsync(Guid userId)
		{
			var all = await _genericRepository.GetAllAsync();
			var lp = all.FirstOrDefault(x => x.UserId == userId);
			return lp?.Points ?? 0;
		}

		public async Task<bool> AddPointsAsync(Guid userId, int points)
		{
			var all = await _genericRepository.GetAllAsync();
			var lp = all.FirstOrDefault(x => x.UserId == userId);

			if (lp == null) return false;

			lp.Points = (lp.Points ?? 0) + points;
			lp.UpdatedAt = DateTime.UtcNow;

			await _genericRepository.UpdateAsync(lp);
			return true;
		}

		public async Task<bool> RedeemPointsAsync(Guid userId, int points)
		{
			var all = await _genericRepository.GetAllAsync();
			var lp = all.FirstOrDefault(x => x.UserId == userId);

			if (lp == null || (lp.Points ?? 0) < points) return false;

			lp.Points -= points;
			lp.UpdatedAt = DateTime.UtcNow;

			await _genericRepository.UpdateAsync(lp);
			return true;
		}
	}
}
