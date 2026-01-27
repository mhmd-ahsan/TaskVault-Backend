using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVault.API.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)] // MySQL-friendly varchar(200)
        public string Title { get; set; }

        [MaxLength(1000)] // MySQL-friendly varchar(1000) instead of nvarchar(max)
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        // ✅ Add this property
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [Required]
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
