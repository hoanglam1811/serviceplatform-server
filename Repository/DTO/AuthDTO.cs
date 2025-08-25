using Microsoft.AspNetCore.Http;
using Repository.Entities;

namespace Repository.DTO;
public class LoginDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class RegisterDTO
{
    public required string Username { get; set; }    
    public required string Password { get; set; }    
    public required string FullName { get; set; }    
    public required string Email { get; set; }
	public required string PhoneNumber { get; set; }
	public required List<IFormFile?> NationalId { get; set; }
    public required string Gender { get; set; }
	public string? Status { get; set; } 
    public string? Role { get; set; }
}
