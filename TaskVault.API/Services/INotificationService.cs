using TaskVault.API.Models;

namespace NotificationApp_New.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Send a notification to a specific user and save it in the database.
        /// </summary>
        /// <param name="notification">The notification object to send</param>
        /// <param name="currentUserId">The ID of the user sending the notification</param>
        /// <returns>The saved notification</returns>
        Task<Notification> SendAsync(Notification notification, string currentUserId);

        /// <summary>
        /// Retrieve all notifications for a specific user.
        /// </summary>
        /// <param name="userId">The recipient user's ID</param>
        /// <returns>List of notifications for the user</returns>
        Task<List<Notification>> GetByUserAsync(string userId);

        /// <summary>
        /// Send a test notification to a specific user (for SignalR testing)
        /// </summary>
        /// <param name="userId">The recipient user's ID</param>
        /// <returns>A completed task</returns>
        Task SendTestNotification(string userId);

        Task SendWelcomeNotificationAsync(string userId, string userName);
    }
}
