using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class LoyaltyPoint
	{
		public Guid Id { get; set; }
		public int? Points { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
