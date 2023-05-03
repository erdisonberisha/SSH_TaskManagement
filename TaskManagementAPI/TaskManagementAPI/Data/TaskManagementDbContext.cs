using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Helpers;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) 
        {
        }

        public DbSet<TaskEntity> Tasks { get; set;}
        public DbSet<SharedTask> SharedTasks { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SharedTask>().HasKey(x => new { x.TaskId, x.UserId });
            modelBuilder.UseIdentityColumns();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            AuthorizationHelper.CreateUserPasswordHash("admin", out byte[] passwordHash, out byte[] passwordSalt);
            modelBuilder.Entity<User>().HasData(
                new User { Email = "admin@admin.com", Name = "admin admin", 
                    DateOfBirth = DateTime.MinValue, PasswordHash = passwordHash, 
                    PasswordSalt = passwordSalt, Username = "admin", Role = "Admin", Id = 1000});
        }
    }
}
