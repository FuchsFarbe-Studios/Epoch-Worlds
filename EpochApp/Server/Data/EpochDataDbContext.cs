// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Server.Controllers;
using EpochApp.Shared.Blog;
using EpochApp.Shared.Config;
using EpochApp.Shared.Lookups;
using EpochApp.Shared.Users;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Data
{
    public class EpochDataDbContext : DbContext
    {
        public string ConnectionString { get; private set; } = @"Data Source=C:\myFolder\myAccessFile.accdb;";
        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSocial> UserSocials { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        // Lookups
        public DbSet<SocialMedia> SocialMediae { get; set; }
        public DbSet<MetaCategory> MetaCategories { get; set; }
        public DbSet<MetaTemplate> MetaTemplates { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Phoneme> Phonemes { get; set; }
        public DbSet<Vowel> Vowels { get; set; }
        public DbSet<Consonant> Consonants { get; set; }
        // Blogs
        public DbSet<BlogType> BlogTypes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogOwner> BlogsOwners { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostType> PostTypes { get; set; }

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

            modelBuilder.Entity<Phoneme>(
                entity =>
                {
                    entity.ToTable("lkPhonemes", "Lookups");
                    entity.HasKey(e => e.PhonemeID);
                    entity.Property(e => e.PhonemeID).HasMaxLength(4);
                    entity.Property(e => e.AudioFile).HasMaxLength(155);
                });

            modelBuilder.Entity<Consonant>(
                entity =>
                {
                    entity.ToTable("lkConsonants", "Lookups");
                    entity.Property(e => e.Manner).HasMaxLength(35);
                    entity.Property(e => e.Place).HasMaxLength(35);
                });

            modelBuilder.Entity<Vowel>(
                entity =>
                {
                    entity.ToTable("lkVowels", "Lookups");
                    entity.Property(e => e.Depth).HasMaxLength(35);
                    entity.Property(e => e.Verticality).HasMaxLength(35);
                });

            // Blogging
            modelBuilder.Entity<BlogOwner>(
                entity =>
                {
                    entity.ToTable("BlogOwners", "Blogs");
                    entity.HasKey(e => new {e.BlogID, e.OwnerID});

                    entity.HasOne(b => b.Blog)
                          .WithMany(b=>b.BlogOwners)
                          .HasForeignKey(b => b.BlogID)
                          .HasConstraintName("FK_BlogOwners_Blogs")
                          .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(b => b.Owner)
                          .WithMany(bp => bp.OwnedBlogs)
                          .HasForeignKey(bp => bp.OwnerID)
                          .HasConstraintName("FK_BlogOwners_Users")
                          .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<Blog>(
                entity =>
                {
                    entity.ToTable("Blogs", "Blogs");
                    entity.HasKey(e => e.BlogID);

                    entity.HasOne(b => b.BlogType)
                          .WithMany()
                          .HasForeignKey(b => b.BlogTypeID)
                          .HasConstraintName("FK_Blogs_BlogTypes");

                    entity.HasMany(b => b.BlogPosts)
                          .WithOne(bp => bp.Blog)
                          .HasForeignKey(bp => bp.BlogID)
                          .HasConstraintName("FK_BlogPosts_Blogs");
                });

            modelBuilder.Entity<BlogType>(
                entity =>
                {
                    entity.ToTable("BlogTypes", "Blogs");
                    entity.HasKey(e => e.BlogTypeID);
                    entity.HasData(
                        new List<BlogType>()
                        {
                            new BlogType { BlogTypeID = 1, Description = "NEWS" },
                            new BlogType { BlogTypeID = 2, Description = "UPDATES" },
                            new BlogType { BlogTypeID = 3, Description = "EVENTS" },
                            new BlogType { BlogTypeID = 4, Description = "FAQ" },
                            new BlogType { BlogTypeID = 5, Description = "DOCUMENTATION" }
                        });
                });

            modelBuilder.Entity<Post>(
                entity =>
                {
                    entity.ToTable("Posts", "Blogs");
                    entity.HasKey(e => e.PostID);

                    entity.HasOne(p => p.Author)
                          .WithMany() // replace with .WithMany(u => u.Posts) if User class have collection of posts
                          .HasForeignKey(p => p.AuthorID);

                    entity.HasMany(p => p.BlogPosts)
                          .WithOne(bp => bp.Post)
                          .HasForeignKey(bp => bp.PostID);
                });

            modelBuilder.Entity<BlogPost>(
                entity =>
                {
                    entity.ToTable("BlogPosts", "Blogs");
                    entity.HasKey(bp => new { bp.BlogID, bp.PostID });

                    entity.HasOne(bp => bp.Blog)
                          .WithMany(b => b.BlogPosts)
                          .HasForeignKey(bp => bp.BlogID)
                          .HasConstraintName("FK_BlogPosts_Blogs");

                    entity.HasOne(bp => bp.Post)
                          .WithMany(p => p.BlogPosts)
                          .HasForeignKey(bp => bp.PostID)
                          .HasConstraintName("FK_BlogPosts_Posts");
                });

            modelBuilder.Entity<PostType>(
                entity =>
                {
                    entity.ToTable("PostTypes", "Blogs");
                    entity.HasKey(e => e.PostTypeID);
                });
        }
    }
}