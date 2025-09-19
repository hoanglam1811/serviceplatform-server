using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class ProviderProfileDTO
	{
		public Guid? Id { get; set; }
		public Guid? UserId { get; set; }
		public string? CompanyName { get; set; }
		public string? Type { get; set; }
		public string? Address { get; set; }
		public string? TaxCode { get; set; }
		public string? BusinessPhone { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public UserDTO? User { get; set; }
	}

	// DTO khi tạo mới
	public class CreateProviderProfileDTO
	{
		public Guid? UserId { get; set; }
		public string? CompanyName { get; set; }
		public string? Type { get; set; }
		public string? Address { get; set; }
		public string? TaxCode { get; set; }
		public string? BusinessPhone { get; set; }
	}

	// DTO khi cập nhật
	public class UpdateProviderProfileDTO
	{
		public Guid Id { get; set; }
		public string? CompanyName { get; set; }
		public string? Type { get; set; }
		public string? Address { get; set; }
		public string? TaxCode { get; set; }
		public string? BusinessPhone { get; set; }
	}
}
