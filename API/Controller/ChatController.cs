using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ChatController : ControllerBase
	{
		private readonly IChatService _chatService;

		public ChatController(IChatService chatService)
		{
			_chatService = chatService;
		}

		// CREATE
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateChatDTO dto)
		{
			try
			{
				var chat = await _chatService.AddAsync(dto);
				return Ok(ApiResponse<ChatDTO>.SuccessResponse(chat, "Message sent successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}

		// GET ALL
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var chats = await _chatService.GetAllAsync();
				return Ok(ApiResponse<IEnumerable<ChatDTO>>.SuccessResponse(chats, "Get all messages successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}

		// GET BY ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var chat = await _chatService.GetByIdAsync(id);
				if (chat == null)
					return NotFound(ApiResponse<string>.Failure("Message not found"));

				return Ok(ApiResponse<ChatDTO>.SuccessResponse(chat, "Get message successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}

		// UPDATE
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChatDTO dto)
		{
			try
			{
				dto.Id = id;
				var chat = await _chatService.UpdateAsync(dto);

				if (chat == null)
					return NotFound(ApiResponse<string>.Failure("Message not found"));

				return Ok(ApiResponse<ChatDTO>.SuccessResponse(chat, "Message updated successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}

		// DELETE
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var chat = await _chatService.DeleteAsync(id);

				if (chat == null)
					return NotFound(ApiResponse<string>.Failure("Message not found"));

				return Ok(ApiResponse<ChatDTO>.SuccessResponse(chat, "Message deleted successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}

		// GET conversation between two users
		[HttpGet("conversation")]
		public async Task<IActionResult> GetConversation([FromQuery] Guid user1, [FromQuery] Guid user2)
		{
			try
			{
				var chats = await _chatService.GetConversationAsync(user1, user2);
				return Ok(ApiResponse<IEnumerable<ChatDTO>>.SuccessResponse(chats, "Conversation loaded successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}

		// MARK messages as read
		[HttpPut("mark-as-read")]
		public async Task<IActionResult> MarkAsRead([FromQuery] Guid senderId, [FromQuery] Guid receiverId)
		{
			try
			{
				await _chatService.MarkAsReadAsync(senderId, receiverId);
				return Ok(ApiResponse<string>.SuccessResponse("Messages marked as read successfully"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
			}
		}
	}
}
