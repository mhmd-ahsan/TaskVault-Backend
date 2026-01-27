using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVault.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // varchar(100)
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)] // varchar(255) for email
        public string EmailAddress { get; set; }

        [Required]
        [Column(TypeName = "longtext")] // MySQL equivalent of nvarchar(max)
        public string PasswordHash { get; set; }

        // Navigation property
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
