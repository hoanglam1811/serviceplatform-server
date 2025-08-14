using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class LoyaltyPointDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public int? Points { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreateLoyaltyPointDTO
	{
		public Guid UserId { get; set; }
		public int? Points { get; set; }
	}

	public class UpdateLoyaltyPointDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public int? Points { get; set; }
	}
}
