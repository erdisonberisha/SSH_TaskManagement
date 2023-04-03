using Microsoft.EntityFrameworkCore;
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
        }
    }
}
