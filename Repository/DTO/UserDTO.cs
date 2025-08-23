using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class UserDTO
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string FullName { get; set; }
		public string? Role { get; set; }
		public string? AvatarUrl { get; set; }
		public string Email { get; set; } = string.Empty;
		public string? PhoneNumber { get; set; }
		public string? Gender { get; set; }
		public string? NationalId { get; set; }
		public string? Address { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	// DTO khi tạo mới
	public class CreateUserDTO
	{
		public string Username { get; set; }  
		public string Password { get; set; } 
		public string FullName { get; set; }
		public string? Role { get; set; }
		public string? AvatarUrl { get; set; }
		public string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Gender { get; set; }
		public IFormFile? NationalId { get; set; }
		public string? Address { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }
	}

	// DTO khi cập nhật
	public class UpdateUserDTO
	{
		public Guid Id { get; set; }
		public string? FullName { get; set; }
		public string? Role { get; set; }
		public string? AvatarUrl { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Gender { get; set; }
		public IFormFile? NationalId { get; set; }
		public string? Address { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }
	}
}
