using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class PayOSCreatePaymentLinkRequest
	{
		public string orderCode { get; set; } = Guid.NewGuid().ToString();
		public string amount { get; set; }
		public string description { get; set; }
		public string cancelUrl { get; set; }
		public string returnUrl { get; set; }
	}

	public class PayOSCreatePaymentLinkResponse
	{
		public string code { get; set; }
		public string desc { get; set; }
		public PayOSPaymentData data { get; set; }
	}

	public class PayOSPaymentData
	{
		public string bin { get; set; }
		public string accountNumber { get; set; }
		public string accountName { get; set; }
		public string amount { get; set; }
		public string description { get; set; }
		public string orderCode { get; set; }
		public string qrCode { get; set; }
		public string paymentLinkId { get; set; }
	}

	public class PayOSPaymentResponseDTO
	{
		public string OrderCode { get; set; }
		public string PaymentUrl { get; set; }
		public string Status { get; set; }
	}

	public class PayOSPaymentRequestDTO
	{
		public string OrderCode { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string ReturnUrl { get; set; }
		public string CancelUrl { get; set; }
	}
}
