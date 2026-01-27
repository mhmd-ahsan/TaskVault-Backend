namespace TaskVault.API.Dtos
{
    public class NotificationDto
    {
        public string RecipientUserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
