using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public required string Username { get; set; }
		public required string PasswordHashed { get; set; } = "";
		public required string FullName { get; set; }
		public string? Role { get; set; }
		public string? AvatarUrl { get; set; }
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Gender { get; set; }
		public string? NationalId { get; set; }
		public string? Address { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public ICollection<ProviderProfile> ProviderProfiles { get; set; } = new List<ProviderProfile>();
	}
}
