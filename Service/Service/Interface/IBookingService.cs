using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface IBookingService : IGenericService<Booking, CreateBookingDTO, UpdateBookingDTO, BookingDTO>
	{

		Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(Guid userId);
		Task<IEnumerable<BookingDTO>> GetBookingsByProviderIdAsync(Guid providerId);
		Task<IEnumerable<BookingDTO>> GetBookingsByServiceIdAsync(Guid serviceId);
		Task<bool> UpdateBookingStatusAsync(Guid bookingId, string status);
	}
}
