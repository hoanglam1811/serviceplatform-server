using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface IChatService
		: IGenericService<Chat, CreateChatDTO, UpdateChatDTO, ChatDTO>
	{
		Task<IEnumerable<ChatDTO>> GetConversationAsync(Guid user1, Guid user2);
		Task MarkAsReadAsync(Guid senderId, Guid receiverId);
	}
}
