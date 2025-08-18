using AutoMapper;
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
	public class PaymentService
		: GenericService<Payment, PaymentDTO, CreatePaymentDTO, UpdatePaymentDTO>, IPaymentService
	{
		public PaymentService(IGenericRepository<Payment> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<IEnumerable<PaymentDTO>> GetPaymentsByBookingIdAsync(Guid bookingId)
		{
			var payments = await _genericRepository.GetAllAsync();
			var result = payments.Where(p => p.BookingId == bookingId);
			return _mapper.Map<IEnumerable<PaymentDTO>>(result);
		}

		public async Task<PaymentDTO?> UpdateStatusAsync(Guid id, string status)
		{
			var payment = await _genericRepository.GetByIdAsync(id);
			if (payment == null) return null;

			payment.Status = status;
			payment.UpdatedAt = DateTime.UtcNow;

			await _genericRepository.UpdateAsync(payment);
			return _mapper.Map<PaymentDTO>(payment);
		}
	}
}
