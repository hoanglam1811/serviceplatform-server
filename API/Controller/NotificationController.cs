using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var result = await _notificationService.GetByUserIdAsync(userId);
			return Ok(ApiResponse<IEnumerable<NotificationDTO>>.SuccessResponse(result, "Notifications retrieved successfully"));
		}

		[HttpPut("{id}/mark-as-read")]
		public async Task<IActionResult> MarkAsRead(Guid id)
		{
			var success = await _notificationService.MarkAsReadAsync(id);
			if (!success)
				return NotFound(ApiResponse<string>.Failure("Notification not found"));

			return Ok(ApiResponse<string>.SuccessResponse("Notification marked as read"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _notificationService.DeleteAsync(id);
			return Ok(ApiResponse<NotificationDTO>.SuccessResponse(result, "Notification deleted successfully"));
		}
	}
}
