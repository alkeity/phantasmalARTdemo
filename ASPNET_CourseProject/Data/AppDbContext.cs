using ASPNET_CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_CourseProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Art> Arts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User model
            modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>().Property(user => user.UpdatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>().Property(user => user.LastLogin).HasDefaultValueSql("GETDATE()");

            // Art model
            modelBuilder.Entity<Art>().Property(art => art.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Art>().Property(art => art.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
