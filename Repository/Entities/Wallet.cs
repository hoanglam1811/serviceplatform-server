using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Wallet
	{
		public Guid Id { get; set; }
		public decimal? Balance { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
