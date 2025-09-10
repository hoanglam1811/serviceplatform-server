using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class ReviewDTO
	{
		public Guid Id { get; set; }
		public Guid BookingId { get; set; }
		public int? Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreateReviewDTO
	{
		public Guid BookingId { get; set; }
		public int? Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime? CreatedAt { get; set; }
	}

	public class UpdateReviewDTO
	{
		public Guid Id { get; set; }
		public Guid BookingId { get; set; }
		public int? Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
