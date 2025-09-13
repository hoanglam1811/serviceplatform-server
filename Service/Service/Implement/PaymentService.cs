using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository.DTO;
using Repository.Entities;
using Repository.Repository.Interface;
using Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Service.Implement
{
	public class PaymentService : GenericService<Payment, CreatePaymentDTO, UpdatePaymentDTO, PaymentDTO>, IPaymentService
	{
		private readonly IPayOSService _payOSService;
		private readonly IMapper _mapper;

		public PaymentService(IPayOSService payOSService, IGenericRepository<Payment> genericRepository, IMapper mapper) : base(genericRepository, mapper) 
		{
			_payOSService = payOSService;
			_mapper = mapper;
		}

		public async Task<PaymentDTO> CreatePayOSPaymentLinkAsync(CreatePaymentDTO dto)
		{
			var payment = await AddAsync(dto);

			var payOsRequest = new PayOSPaymentRequestDTO
			{
				amount = dto.Amount,
				orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				returnUrl = "https://your-website.com/payment/success",
				cancelUrl = "https://your-website.com/payment/cancel",
				description = $"Thanh toán booking {dto.BookingId}"
			};

			var response = await _payOSService.CreatePaymentAsync(payOsRequest);

			payment.PaymentUrl = response.checkoutUrl;

			var entity = await _genericRepository.GetByIdAsync(payment.Id);
			entity.PaymentUrl = payment.PaymentUrl;
			await _genericRepository.UpdateAsync(entity);

			return _mapper.Map<PaymentDTO>(entity);

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
