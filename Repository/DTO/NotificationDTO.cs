using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class NotificationDTO
	{
		public Guid? Id { get; set; }
		public string? Title { get; set; }
		public string? Message { get; set; }
		public Guid? UserId { get; set; }
		public string? Status { get; set; } 
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreateNotificationDTO
	{
		public string? Title { get; set; }
		public string? Message { get; set; }
		public Guid? UserId { get; set; }
		public string? Status { get; set; }
	}

	public class UpdateNotificationDTO
	{
		public Guid Id { get; set; }
		public string? Title { get; set; }
		public string? Message { get; set; }
		public string? Status { get; set; }
	}
}
