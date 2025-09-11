using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService _bookingService;

		public BookingController(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		// CREATE
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateBookingDTO dto)
		{
			var booking = await _bookingService.AddAsync(dto);
			return Ok(ApiResponse<BookingDTO>.SuccessResponse(booking, "Booking created successfully"));
		}

		// READ - Get all
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var bookings = await _bookingService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<BookingDTO>>.SuccessResponse(bookings, "Get all bookings successfully"));
		}

		// READ - Get by Id
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var booking = await _bookingService.GetByIdAsync(id);
			if (booking == null)
				return NotFound(ApiResponse<string>.Failure("Booking not found"));

			return Ok(ApiResponse<BookingDTO>.SuccessResponse(booking, "Get booking successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingDTO dto)
		{
			dto.Id = id;
			var booking = await _bookingService.UpdateAsync(dto);
			if (booking == null)
				return NotFound(ApiResponse<string>.Failure("Booking not found"));

			return Ok(ApiResponse<BookingDTO>.SuccessResponse(booking, "Booking updated successfully"));
		}

		// DELETE
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var booking = await _bookingService.DeleteAsync(id);
			if (booking == null)
				return NotFound(ApiResponse<string>.Failure("Booking not found"));

			return Ok(ApiResponse<BookingDTO>.SuccessResponse(booking, "Booking deleted successfully"));
		}

		// GET by UserId
		[HttpGet("user/{userId:guid}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
			return Ok(ApiResponse<IEnumerable<BookingDTO>>.SuccessResponse(bookings, "Get bookings by user successfully"));
		}

		// GET by ServiceId
		[HttpGet("service/{serviceId:guid}")]
		public async Task<IActionResult> GetByServiceId(Guid serviceId)
		{
			var bookings = await _bookingService.GetBookingsByServiceIdAsync(serviceId);
			return Ok(ApiResponse<IEnumerable<BookingDTO>>.SuccessResponse(bookings, "Get bookings by service successfully"));
		}

    // GET by ProviderId
		[HttpGet("provider/{providerId:guid}")]
		public async Task<IActionResult> GetByProviderId(Guid providerId)
		{
			var bookings = await _bookingService.GetBookingsByProviderIdAsync(providerId);
			return Ok(ApiResponse<IEnumerable<BookingDTO>>.SuccessResponse(bookings, "Get bookings by service successfully"));
		}

		// UPDATE STATUS
		[HttpPut("{id:guid}/status")]
		public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] string status)
		{
			var success = await _bookingService.UpdateBookingStatusAsync(id, status);
			if (!success)
				return NotFound(ApiResponse<string>.Failure("Booking not found"));

			return Ok(ApiResponse<string>.SuccessResponse("Booking status updated successfully"));
		}
	}
}
