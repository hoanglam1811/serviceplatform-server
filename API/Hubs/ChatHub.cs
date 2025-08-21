using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;

namespace API.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string sender, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", sender, message);
		}

		public async Task SendPrivateMessage(string receiverId, string message)
		{
			await Clients.User(receiverId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
		}
	}	
}
