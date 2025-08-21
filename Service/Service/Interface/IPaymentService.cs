using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface IPaymentService: IGenericService<Payment, CreatePaymentDTO, UpdatePaymentDTO, PaymentDTO>
	{
		Task<PaymentDTO> CreatePayOSPaymentLinkAsync(CreatePaymentDTO dto);
		Task<IEnumerable<PaymentDTO>> GetPaymentsByBookingIdAsync(Guid bookingId);
		Task<PaymentDTO?> UpdateStatusAsync(Guid id, string status);

	}
}
