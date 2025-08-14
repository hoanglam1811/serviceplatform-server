using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class BookingDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid ServiceId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string? Status { get; set; }
		public string? PaymentStatus { get; set; }
		public string? Note { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreateBookingDTO
	{
		public Guid UserId { get; set; }
		public Guid ServiceId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string? Status { get; set; }
		public string? PaymentStatus { get; set; }
		public string? Note { get; set; }
	}

	public class UpdateBookingDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid ServiceId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string? Status { get; set; }
		public string? PaymentStatus { get; set; }
		public string? Note { get; set; }
	}
}
