using Microsoft.EntityFrameworkCore;
using UserTaskApi.DataAccess;

namespace DataAccess.Context
{
    public class UserTaskContext(DbContextOptions<UserTaskContext> options) : DbContext(options)
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<UserTaskDdModel> Tasks { get; set; }
        public DbSet<UserTaskHistoryDbModel> TasksHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDbModel>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired();
                entity.HasIndex(b => b.Name).IsUnique();
                entity.HasMany(e => e.Tasks).WithOne(e => e.AssignedUser).HasForeignKey(e => e.UserId);
                entity.HasMany(e => e.TaskHistory).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<UserTaskDdModel>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.HasMany(e => e.TaskHistory).WithOne(e => e.Task).HasForeignKey(e => e.TaskId);
            });

            modelBuilder.Entity<UserTaskHistoryDbModel>(entity =>
            {
                entity.HasKey(b => b.Id);
            });
        }
    }
}
