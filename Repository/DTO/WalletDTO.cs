using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class WalletDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public decimal? Balance { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreateWalletDTO
	{
		public Guid UserId { get; set; }
		public decimal? Balance { get; set; }
	}

	public class UpdateWalletDTO
	{
		public Guid Id { get; set; }
		public decimal? Balance { get; set; }
	}
}
