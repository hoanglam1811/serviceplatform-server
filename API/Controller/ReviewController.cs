using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReviewController : ControllerBase
	{
		private readonly IReviewService _reviewService;

		public ReviewController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _reviewService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ReviewDTO>>.SuccessResponse(result, "Reviews retrieved successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _reviewService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<ReviewDTO>.Failure("Review not found"));

			return Ok(ApiResponse<ReviewDTO>.SuccessResponse(result, "Review retrieved successfully"));
		}

		[HttpGet("booking/{bookingId:guid}")]
		public async Task<IActionResult> GetByBookingId(Guid bookingId)
		{
			var result = await _reviewService.GetByBookingIdAsync(bookingId);
			return Ok(ApiResponse<IEnumerable<ReviewDTO>>.SuccessResponse(result, "Reviews by booking retrieved successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateReviewDTO dto)
		{
			var result = await _reviewService.AddAsync(dto);
			return Ok(ApiResponse<ReviewDTO>.SuccessResponse(result, "Review created successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewDTO dto)
		{
			dto.Id = id;
			var result = await _reviewService.UpdateAsync(dto);
			return Ok(ApiResponse<ReviewDTO>.SuccessResponse(result, "Review updated successfully"));
		}


		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _reviewService.DeleteAsync(id);
			return Ok(ApiResponse<ReviewDTO>.SuccessResponse(result, "Review deleted successfully"));
		}
	}
}
