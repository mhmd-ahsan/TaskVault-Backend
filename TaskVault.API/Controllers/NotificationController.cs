using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NotificationApp_New.Services;
using TaskVault.API.Dtos;

namespace NotificationApp_New.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        // ✅ Inject interface, not concrete class
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDto dto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notification = new Notification
            {
                Title = dto.Title,
                Message = dto.Message,
                SentTo = dto.RecipientUserId
            };

            await _notificationService.SendAsync(notification, currentUserId);

            return Ok("Notification sent!");
        }

        [HttpPost("test/{userId}")]
        public async Task<IActionResult> SendTest(string userId)
        {
            await _notificationService.SendTestNotification(userId);
            return Ok("Test notification sent!");
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = await _notificationService.GetByUserAsync(userId);
            return Ok(notifications);
        }
    }
}
