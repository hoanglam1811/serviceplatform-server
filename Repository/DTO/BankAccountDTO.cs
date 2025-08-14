using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class BankAccountDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string? BankName { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountHolderName { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	public class CreateBankAccountDTO
	{
		public Guid UserId { get; set; }
		public string? BankName { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountHolderName { get; set; }
	}

	public class UpdateBankAccountDTO
	{
		public Guid Id { get; set; }
		public string? BankName { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountHolderName { get; set; }
	}
}
