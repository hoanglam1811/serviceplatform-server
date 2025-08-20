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
	public class BookingService : GenericService<Booking, CreateBookingDTO, UpdateBookingDTO, BookingDTO>, IBookingService
	{
		public BookingService(IGenericRepository<Booking> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(Guid userId)
		{
			var bookings = await _genericRepository.GetAllAsync(
				q => q.Include(b => b.ServiceId)
			);

			bookings = bookings.Where(b => b.UserId == userId).ToList();
			return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
		}

		public async Task<IEnumerable<BookingDTO>> GetBookingsByServiceIdAsync(Guid serviceId)
		{
			var bookings = await _genericRepository.GetAllAsync(
				q => q.Include(b => b.User)
			);

			bookings = bookings.Where(b => b.ServiceId == serviceId).ToList();
			return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
		}

		public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, string status)
		{
			var booking = await _genericRepository.GetByIdAsync(bookingId);
			if (booking == null) return false;

			booking.Status = status;
			booking.UpdatedAt = DateTime.UtcNow;

			await _genericRepository.UpdateAsync(booking);
			return true;
		}
	}
}
