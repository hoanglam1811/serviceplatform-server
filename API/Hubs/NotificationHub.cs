using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;

namespace API.Hubs
{
	public class NotificationHub : Hub
	{
		public async Task SendNotification(string userId, string message)
		{
			await Clients.User(userId).SendAsync("ReceiveNotification", message);
		}
	}
}
