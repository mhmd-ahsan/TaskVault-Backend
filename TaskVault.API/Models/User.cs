

using System.ComponentModel.DataAnnotations;

namespace TaskVault.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
