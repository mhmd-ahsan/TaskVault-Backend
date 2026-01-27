using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TaskVault.API.Models;
using NotificationApp_New.Hubs;
using TaskVault.API.Data;

namespace NotificationApp_New.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(
            AppDbContext context,
            IHubContext<NotificationHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        // Send a notification to a user and save it in DB
        public async Task<Notification> SendAsync(Notification notification, string currentUserId)
        {
            notification.Id = Guid.NewGuid();
            notification.SentBy = currentUserId;
            notification.CreatedAt = DateTime.UtcNow;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send via SignalR
            await _hub.Clients.User(notification.SentTo)
                .SendAsync("ReceiveNotification", new
                {
                    notification.Id,
                    notification.Title,
                    notification.Message,
                    notification.SentBy,
                    notification.CreatedAt
                });

            return notification;
        }

        // Get notifications for a specific user
        public async Task<List<Notification>> GetByUserAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.SentTo == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        // ✅ Optional: send a test notification to a user
        public async Task SendTestNotification(string userId)
        {
            var testNotification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = "Test Notification",
                Message = "SignalR is working!",
                SentBy = "System",
                SentTo = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(testNotification);
            await _context.SaveChangesAsync();

            await _hub.Clients.User(userId)
                .SendAsync("ReceiveNotification", new
                {
                    testNotification.Id,
                    testNotification.Title,
                    testNotification.Message,
                    testNotification.SentBy,
                    testNotification.CreatedAt
                });
        }

        public async Task SendWelcomeNotificationAsync(string userId, string userName)
        {
            var notification = new Notification
            {
                //Id = Guid.NewGuid(),
                Title = "Welcome!",
                Message = $"Hello {userName}, welcome back!",
                SentTo = userId
                //SentBy = userId
            };

            // Call your existing SendAsync method
            await SendAsync(notification, userId.ToString());
        }

    }
}
