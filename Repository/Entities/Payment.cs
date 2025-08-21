using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Payment
	{
		public Guid Id { get; set; }
		public decimal? Amount { get; set; }
		public string? Method { get; set; }
		public string? Status { get; set; }
		public DateTime? TransactionDate { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string? PaymentUrl { get; set; }

		public Guid BookingId { get; set; }
		public Booking Booking { get; set; }
	}
}
