using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DTO;

namespace Service.Service.Interface
{
	public interface IPayOSService
	{
		Task<PayOSPaymentResponseDTO> CreatePaymentAsync(PayOSPaymentRequestDTO request);
		Task<PayOSPaymentResponseDTO> GetPaymentStatusAsync(string orderCode);
		Task<bool> VerifyWebhookAsync(string signature, string payload);
	}
}
