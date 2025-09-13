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
		public long orderCode { get; set; }
		public string checkoutUrl { get; set; }
		public string status { get; set; }
	}

	public class PayOSPaymentRequestDTO
	{
		public long orderCode { get; set; }
		public decimal amount { get; set; }
		public string description { get; set; }
		public string returnUrl { get; set; }
		public string cancelUrl { get; set; }
    public string signature { get; set; }
	}

  public class PayOSWebhook
  {
    public string code { get; set; }
    public string desc { get; set; }
    public bool success { get; set; }
    public PayOSWebhookData data { get; set; }
    public string signature { get; set; }
  }

  public class PayOSWebhookData
  {
    public long orderCode { get; set; }
    public decimal amount { get; set; }
		public string description { get; set; }
		public string accountNumber { get; set; }
		public string reference { get; set; }
    public string transactionDateTime { get; set; }
    public string currency { get; set; }
    public string paymentLinkId { get; set; }
    public string code { get; set; }
    public string desc { get; set; }
    public string counterAccountBankId { get; set; } = "";
    public string counterAccountBankName { get; set; } = "";
    public string counterAccountBankNumber { get; set; } = "";
    public string virtualAccountName { get; set; } = "";
    public string virtualAccountNumber { get; set; } = "";
  }
}
