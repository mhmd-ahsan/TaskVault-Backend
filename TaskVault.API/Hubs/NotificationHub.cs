using Microsoft.AspNetCore.SignalR;
using TaskVault.API.Models;


namespace NotificationApp_New.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotificationToUser(Notification notification)
        {
            await Clients.User(notification.SentTo)
                .SendAsync("ReceiveNotification", notification);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public async Task Broadcast(Notification notification)
        {
            await Clients.All
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
