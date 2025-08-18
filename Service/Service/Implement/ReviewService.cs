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
	public class ReviewService
		: GenericService<Review, CreateReviewDTO, UpdateReviewDTO, ReviewDTO>, IReviewService
	{
		public ReviewService(IGenericRepository<Review> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<IEnumerable<ReviewDTO>> GetByBookingIdAsync(Guid bookingId)
		{
			var reviews = await _genericRepository.GetAllAsync();
			var filtered = reviews.Where(r => r.BookingId == bookingId);
			return _mapper.Map<IEnumerable<ReviewDTO>>(filtered);
		}
	}
}
