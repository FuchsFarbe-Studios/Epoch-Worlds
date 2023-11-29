// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Config;
using EpochApp.Shared.Lookups;
using EpochApp.Shared.Users;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Data
{
    public class EpochDataDbContext : DbContext
    {
        public string ConnectionString { get; private set; } = @"Data Source=C:\myFolder\myAccessFile.accdb;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSocial> UserSocials { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<MetaCategory> MetaCategories { get; set; }
        public DbSet<MetaTemplate> MetaTemplates { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<SocialMedia> SocialMediae { get; set; }

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
                    user.Property(p => p.UserName)
                        .HasMaxLength(64);
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

            modelBuilder.Entity<UserSocial>(
                us =>
                {
                    us.HasKey(x => new { x.SocialID, x.UserID });

                    us.HasOne(x => x.Social)
                      .WithMany()
                      .HasForeignKey(x => x.SocialID);

                    us.HasOne(x => x.Profile)
                      .WithMany(p => p.Socials)
                      .HasForeignKey(x => x.UserID);
                });

            modelBuilder.Entity<Profile>(
                entity =>
                {
                    entity.HasKey(p => p.UserID);
                    entity.HasOne(p => p.User)
                          .WithOne(u => u.Profile)
                          .HasForeignKey<Profile>(p => p.UserID)
                          .HasConstraintName("FK_Profile_User")
                          .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<MetaTemplate>(
                entity =>
                {
                    entity.ToTable("MetaTemplates", "Config");
                    entity.HasKey(e => e.TemplateID);
                    entity.Property(e => e.TemplateID).ValueGeneratedOnAdd();
                    entity.Property(e => e.TemplateName).HasColumnName("Name");
                    entity.Property(e => e.TemplateName).HasMaxLength(50);
                    entity.Property(e => e.Description).HasMaxLength(255);
                    entity.Property(e => e.HelpText).HasMaxLength(150);
                    entity.Property(e => e.Placeholder).HasMaxLength(150);
                    entity.HasOne(e => e.Category)
                          .WithMany()
                          .HasForeignKey(e => e.CategoryID)
                          .HasConstraintName("FK_MetaTemplates_MetaCategories")
                          .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<MetaCategory>(
                entity =>
                {
                    entity.ToTable("lkMetaCategories", "Lookups");
                    entity.HasKey(m => m.CategoryID);
                    entity.Property(m => m.CategoryID).ValueGeneratedOnAdd();
                    entity.Property(m => m.Description).HasColumnName("Description");
                    entity.Property(m => m.Description).HasMaxLength(50);
                });

            modelBuilder.Entity<ArticleCategory>(
                entity =>
                {
                    entity.ToTable("lkArticleCategories", "Lookups");
                    entity.HasKey(m => m.CategoryID);
                    entity.Property(m => m.CategoryID).ValueGeneratedOnAdd();
                    entity.Property(m => m.Description).HasColumnName("Description");
                    entity.Property(m => m.Description).HasMaxLength(50);
                });

            modelBuilder.Entity<SocialMedia>(
                entity =>
                {
                    entity.ToTable("lkSocialMediae", "Lookups");
                    entity.HasKey(m => m.SocialID);
                    entity.Property(m => m.SocialID).ValueGeneratedOnAdd();
                    entity.Property(m => m.SocialMediaName).HasColumnName("Description");
                    entity.Property(m => m.SocialMediaName).HasMaxLength(100);
                    entity.Property(m => m.URLAffix).HasMaxLength(255);
                    entity.Property(m => m.Icon).HasMaxLength(100);
                });
        }
    }
}