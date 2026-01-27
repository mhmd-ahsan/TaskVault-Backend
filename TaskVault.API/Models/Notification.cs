using System.ComponentModel.DataAnnotations;

public class Notification
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(36)]
    public string SentTo { get; set; }

    [MaxLength(100)]
    public string SentBy { get; set; }

    [MaxLength(200)]
    public string Title { get; set; }

    [MaxLength(1000)]
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; }
}
