// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Data
{
    public class EpochDataDbContext : DbContext
    {
        public string ConnectionString { get; private set; } = @"Data Source=C:\myFolder\myAccessFile.accdb;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public EpochDataDbContext(DbContextOptions<EpochDataDbContext> options) : base(options)
        {
        }

        public EpochDataDbContext(string connectionString) : base()
        {
            ConnectionString = connectionString;
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseJet(ConnectionString);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Users");
            modelBuilder.Entity<User>(
                user =>
                {
                    user.Ignore(p => p.Roles);
                    user.Ignore(p => p.Age);
                    user.Property(p => p.UserName)
                        .HasMaxLength(64);
                    user.Property(p => p.Password)
                        .HasMaxLength(128);
                    user.Property(p => p.DateOfBirth)
                        .IsRequired();
                });
            modelBuilder.Entity<UserRole>(
                entity =>
                {
                    entity.HasKey(ur => new { ur.RoleID, ur.UserID });
                    entity.HasOne(ur => ur.User)
                          .WithMany(u => u.UserRoles)
                          .HasForeignKey(ur => ur.UserID);
                    entity.HasOne(ur => ur.Role)
                          .WithMany(r => r.Users)
                          .HasForeignKey(ur => ur.RoleID);
                });

            modelBuilder.Entity<Role>().HasData(
                new List<Role>()
                {
                    new Role { RoleID = 1, Description = "DEFAULT", },
                    new Role { RoleID = 2, Description = "ACTIVATED", },
                    new Role { RoleID = 3, Description = "MODERATOR", },
                    new Role { RoleID = 4, Description = "ADMIN", }
                });
        }
    }
}