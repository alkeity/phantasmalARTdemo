using ASPNET_CourseProject.Data.Models;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_CourseProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Art> Arts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User model
            modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.CreatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<User>().Property(user => user.UpdatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<User>().Property(user => user.LastLogin).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<User>().Property(user => user.RoleID).HasDefaultValue(1);
            modelBuilder.Entity<User>().Property(user => user.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(user => user.IsBanned).HasDefaultValue(false);

            // Art model
            modelBuilder.Entity<Art>().Property(art => art.CreatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Art>().Property(art => art.UpdatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Art>().Property(art => art.IsDeleted).HasDefaultValue(false);
        }
    }
}
