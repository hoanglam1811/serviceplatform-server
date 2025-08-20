using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class LoyaltyPointsController : ControllerBase
	{
		private readonly ILoyaltyService _loyaltyService;

		public LoyaltyPointsController(ILoyaltyService loyaltyService)
		{
			_loyaltyService = loyaltyService;
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetPointsByUserId(Guid userId)
		{
			var result = await _loyaltyService.GetPointsByUserIdAsync(userId);
			return Ok(ApiResponse<int?>.SuccessResponse(result, "Points retrieved successfully"));
		}

		[HttpPost("{userId}/add")]
		public async Task<IActionResult> AddPoints(Guid userId, [FromQuery] int points)
		{
			var success = await _loyaltyService.AddPointsAsync(userId, points);
			if (!success)
				return NotFound(ApiResponse<string>.Failure("User not found to add points"));

			return Ok(ApiResponse<string>.SuccessResponse("Points added successfully"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _loyaltyService.DeleteAsync(id);
			return Ok(ApiResponse<LoyaltyPointDTO>.SuccessResponse(result, "LoyaltyPoint deleted successfully"));
		}
	}
}
