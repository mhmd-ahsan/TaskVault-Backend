using Microsoft.EntityFrameworkCore;
using TaskVault.API.Models;

namespace TaskVault.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Unique Email Addresss
            modelBuilder.Entity<User>().
                HasIndex(u => u.EmailAddress).
                IsUnique();

            // Configure relationship between TaskItem and User
            modelBuilder.Entity<TaskItem>().
                HasOne(t  => t.User).
                WithMany(u => u.Tasks).
                HasForeignKey(t => t.UserId).
                OnDelete(DeleteBehavior.Cascade);

        }
    }
}
