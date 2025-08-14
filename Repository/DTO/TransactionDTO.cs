using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
		public class TransactionDTO
		{
			public Guid Id { get; set; }
			public Guid WalletId { get; set; }
			public decimal? Amount { get; set; }
			public string? Currency { get; set; }
			public string? Status { get; set; }
			public DateTime? CreatedAt { get; set; }
			public DateTime? CompletedAt { get; set; }
		}

		public class CreateTransactionDTO
		{
			public Guid WalletId { get; set; }
			public decimal? Amount { get; set; }
			public string? Currency { get; set; }
			public string? Status { get; set; }
		}

		public class UpdateTransactionDTO
		{
			public Guid Id { get; set; }
			public decimal? Amount { get; set; }
			public string? Currency { get; set; }
			public string? Status { get; set; }
			public DateTime? CompletedAt { get; set; }
		}
	}
}
