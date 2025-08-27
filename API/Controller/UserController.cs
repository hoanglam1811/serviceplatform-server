using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _userService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<UserDTO>>.SuccessResponse(result, "Fetched all users successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _userService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<UserDTO>.Failure("User not found"));
			return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "Fetched user successfully"));
		}

		[HttpGet("by-username/{username}")]
		public async Task<IActionResult> GetByUsername(string username)
		{
			var result = await _userService.GetByUsernameAsync(username);
			if (result == null)
				return NotFound(ApiResponse<UserDTO>.Failure("User not found"));
			return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "Fetched user by username successfully"));
		}

		[HttpGet("role/{role}")]
		public async Task<IActionResult> GetByRole(string role)
		{
			var users = await _userService.GetAllAsync();
			var filtered = users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));
			return Ok(ApiResponse<IEnumerable<UserDTO>>.SuccessResponse(filtered, $"Fetched all {role}s successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
		{
			var result = await _userService.AddAsync(dto);
			return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "User created successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDTO dto)
		{
			dto.Id = id;
			var result = await _userService.UpdateAsync(dto);
			return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "User updated successfully"));
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _userService.DeleteAsync(id);
			return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "User deleted successfully"));
		}
	}
}
