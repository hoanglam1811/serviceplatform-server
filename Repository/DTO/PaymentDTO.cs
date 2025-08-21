using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class PaymentDTO
	{
		public Guid Id { get; set; }
		public Guid BookingId { get; set; }
		public decimal? Amount { get; set; }
		public string? Method { get; set; }
		public string? PaymentUrl { get; set; }
		public string? Status { get; set; }
		public DateTime? TransactionDate { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreatePaymentDTO
	{
		public Guid BookingId { get; set; }
		public decimal Amount { get; set; }
		public string? Method { get; set; }
		public string? Status { get; set; }
		public string? PaymentUrl { get; set; }
		public DateTime? TransactionDate { get; set; }
	}

	public class UpdatePaymentDTO
	{
		public Guid Id { get; set; }
		public Guid BookingId { get; set; }
		public decimal? Amount { get; set; }
		public string? Method { get; set; }
		public string? Status { get; set; }
		public string? PaymentUrl { get; set; }
		public DateTime? TransactionDate { get; set; }
	}
}
