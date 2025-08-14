using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Chat
	{
		public Guid? Id { get; set; }
		public string? Message { get; set; }
		public string? Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid? SenderId { get; set; }
		public Guid? ReceiverId { get; set; }
		public User Sender { get; set; }
		public User Receiver { get; set; }

	}
}
