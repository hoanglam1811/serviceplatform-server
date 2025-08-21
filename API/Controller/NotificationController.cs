using API.DTO;
using API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;
		private readonly IHubContext<NotificationHub> _hubContext;

		public NotificationController(INotificationService notificationService, IHubContext<NotificationHub> hubContext )
		{
			_notificationService = notificationService;
			_hubContext = hubContext;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateNotificationDTO dto)
		{
			var notification = await _notificationService.AddAsync(dto);

			// push tới user qua SignalR
			await _hubContext.Clients.User(dto.UserId.ToString())
				.SendAsync("ReceiveNotification", notification);

			return Ok(ApiResponse<NotificationDTO>.SuccessResponse(notification, "Notification created & pushed successfully"));
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
