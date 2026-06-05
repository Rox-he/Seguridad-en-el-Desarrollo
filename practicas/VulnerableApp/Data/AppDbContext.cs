using Microsoft.EntityFrameworkCore;
using VulnerableApp.Models;

namespace VulnerableApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "$2a$11$5IJFbMBGrZCxhkXFOFaQqeUeABCDEFGHIJKLMNOPQRSTUVWXYZ1234",
                    Email = "admin@test.com",
                    Balance = 1000m,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    PasswordHash = "$2a$11$5IJFbMBGrZCxhkXFOFaQqeUeABCDEFGHIJKLMNOPQRSTUVWXYZ5678",
                    Email = "user@test.com",
                    Balance = 500m,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = 3,
                    Username = "user2",
                    PasswordHash = "$2a$11$5IJFbMBGrZCxhkXFOFaQqeUeABCDEFGHIJKLMNOPQRSTUVWXYZ9012",
                    Email = "user2@test.com",
                    Balance = 750m,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            );
        }
    }
}