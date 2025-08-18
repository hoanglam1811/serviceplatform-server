using AutoMapper;
using Repository.DTO;
using Repository.Entities;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Service.Interface;

namespace Service.Service.Implement
{
	public class ChatService
		: GenericService<Chat, CreateChatDTO, UpdateChatDTO, ChatDTO>, IChatService
	{
		public ChatService(IGenericRepository<Chat> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		/// <summary>
		/// Lấy toàn bộ tin nhắn giữa 2 user (theo thứ tự thời gian)
		/// </summary>
		public async Task<IEnumerable<ChatDTO>> GetConversationAsync(Guid user1, Guid user2)
		{
			var chats = await _genericRepository.GetAllAsync(
				q => q.Where(c =>
						(c.SenderId == user1 && c.ReceiverId == user2) ||
						(c.SenderId == user2 && c.ReceiverId == user1))
					  .OrderBy(c => c.CreatedAt)
			);

			return _mapper.Map<IEnumerable<ChatDTO>>(chats);
		}

		/// <summary>
		/// Đánh dấu tất cả tin nhắn từ user gửi đến mình thành "Đã đọc"
		/// </summary>
		public async Task MarkAsReadAsync(Guid senderId, Guid receiverId)
		{
			var unreadChats = await _genericRepository.GetAllAsync(
				q => q.Where(c => c.SenderId == senderId
							   && c.ReceiverId == receiverId
							   && c.Status != "Read")
			);

			foreach (var chat in unreadChats)
			{
				chat.Status = "Read";
				chat.UpdatedAt = DateTime.UtcNow;
				await _genericRepository.UpdateAsync(chat);
			}
		}
	}
}
