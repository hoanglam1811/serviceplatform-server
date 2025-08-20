using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Booking
	{
		public Guid Id { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string? Status { get; set; }
		public string? PaymentStatus { get; set; }
		public string? Note { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid ServiceId { get; set; }
		public Services Service { get; set; }
	}
}
