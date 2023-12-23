// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using EpochApp.Shared.Config;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Data;

public class EpochDataDbContext : DbContext
{

    public EpochDataDbContext(DbContextOptions<EpochDataDbContext> options) : base(options)
    {
    }

    public EpochDataDbContext(string connectionString) : base()
    {
        ConnectionString = connectionString;
    }
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
    // Worlds
    public DbSet<World> Worlds { get; set; }
    public DbSet<WorldMeta> WorldMetas { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");
        modelBuilder.Entity<User>(user =>
        {
            user.Property(p => p.UserName)
                .HasMaxLength(64);
            user.Property(p => p.Email)
                .HasMaxLength(128);
            user.Ignore(x => x.NormalizedUserName);
            user.Ignore(x => x.NormalizedEmail);
            user.HasMany(u => u.OwnedWorlds)
                .WithOne(w => w.Owner)
                .HasForeignKey(w => w.OwnerID)
                .HasConstraintName("FK_Worlds_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => new
                                {
                                    ur.RoleID, ur.UserID
                                });
            entity.HasOne(ur => ur.User)
                  .WithMany(u => u.UserRoles)
                  .HasForeignKey(ur => ur.UserID)
                  .HasConstraintName("FK_UserRoles_Users")
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(ur => ur.Role)
                  .WithMany(r => r.Users)
                  .HasForeignKey(ur => ur.RoleID)
                  .HasConstraintName("FK_UserRoles_Roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasData(
            new List<Role>()
            {
                new Role
                {
                    RoleID = 1, Description = "DEFAULT",
                },
                new Role
                {
                    RoleID = 2, Description = "ACTIVATED",
                },
                new Role
                {
                    RoleID = 3, Description = "MODERATOR",
                },
                new Role
                {
                    RoleID = 4, Description = "ADMIN",
                }
            });
        });


        modelBuilder.Entity<UserSocial>(us =>
        {
            us.HasKey(x => new
                           {
                               x.SocialID, x.UserID
                           });

            us.HasOne(x => x.Social)
              .WithMany()
              .HasForeignKey(x => x.SocialID)
              .HasConstraintName("FK_UserSocials_SocialMediae");

            us.HasOne(x => x.Profile)
              .WithMany(p => p.Socials)
              .HasForeignKey(x => x.UserID)
              .HasConstraintName("FK_UserSocials_Users")
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(p => p.UserID);
            entity.HasOne(p => p.User)
                  .WithOne(u => u.Profile)
                  .HasForeignKey<Profile>(p => p.UserID)
                  .HasConstraintName("FK_Profile_User")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MetaTemplate>(entity =>
        {
            entity.ToTable("lkMetaTemplates", "Lookups");
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

        modelBuilder.Entity<MetaCategory>(entity =>
        {
            entity.ToTable("lkMetaCategories", "Lookups");
            entity.HasKey(m => m.CategoryID);
            entity.Property(m => m.CategoryID).ValueGeneratedOnAdd();
            entity.Property(m => m.Description).HasColumnName("Description");
            entity.Property(m => m.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<ArticleCategory>(entity =>
        {
            entity.ToTable("lkArticleCategories", "Lookups");
            entity.HasKey(m => m.CategoryID);
            entity.Property(m => m.CategoryID).UseJetIdentityColumn(3, 4);
            entity.Property(m => m.Description).HasColumnName("Description");
            entity.Property(m => m.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<SocialMedia>(entity =>
        {
            entity.ToTable("lkSocialMediae", "Lookups");
            entity.HasKey(m => m.SocialID);
            entity.Property(m => m.SocialID).UseJetIdentityColumn(12, 12);
            entity.Property(m => m.SocialMediaName).HasColumnName("Description");
            entity.Property(m => m.SocialMediaName).HasMaxLength(100);
            entity.Property(m => m.URLAffix).HasMaxLength(255);
            entity.Property(m => m.Icon).HasMaxLength(100);
        });

        modelBuilder.Entity<Phoneme>(entity =>
        {
            entity.ToTable("lkPhonemes", "Lookups");
            entity.HasKey(e => e.PhonemeID);
            entity.Property(e => e.PhonemeID).HasMaxLength(4).ValueGeneratedNever();
            entity.Property(e => e.AudioFile).HasMaxLength(155);
        });

        modelBuilder.Entity<Consonant>(entity =>
        {
            entity.ToTable("lkConsonants", "Lookups");
            entity.Property(e => e.Manner).HasMaxLength(35);
            entity.Property(e => e.Place).HasMaxLength(35);
        });

        modelBuilder.Entity<Vowel>(entity =>
        {
            entity.ToTable("lkVowels", "Lookups");
            entity.Property(e => e.Depth).HasMaxLength(35);
            entity.Property(e => e.Verticality).HasMaxLength(35);
        });

        // Blogging
        modelBuilder.Entity<BlogOwner>(entity =>
        {
            entity.ToTable("BlogOwners", "Blogs");
            entity.HasKey(e => new { e.BlogID, e.OwnerID });
            entity.HasOne(b => b.Blog)
                  .WithMany(b => b.BlogOwners)
                  .HasForeignKey(b => b.BlogID)
                  .HasConstraintName("FK_BlogOwners_Blogs");
            entity.HasOne(b => b.Owner)
                  .WithMany(bp => bp.OwnedBlogs)
                  .HasForeignKey(bp => bp.OwnerID)
                  .HasConstraintName("FK_BlogOwners_Users")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Blog>(entity =>
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

        modelBuilder.Entity<BlogType>(entity =>
        {
            entity.ToTable("lkBlogTypes", "Blogs");
            entity.HasKey(e => e.BlogTypeID);
            entity.HasData(new List<BlogType>()
                           {
                               new BlogType
                               {
                                   BlogTypeID = 1, Description = "NEWS"
                               },
                               new BlogType
                               {
                                   BlogTypeID = 2, Description = "UPDATES"
                               },
                               new BlogType
                               {
                                   BlogTypeID = 3, Description = "EVENTS"
                               },
                               new BlogType
                               {
                                   BlogTypeID = 4, Description = "FAQ"
                               },
                               new BlogType
                               {
                                   BlogTypeID = 5, Description = "DOCUMENTATION"
                               }
                           });
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Posts", "Blogs");
            entity.HasKey(e => e.PostID);
            entity.HasOne(p => p.Author)
                  .WithMany()// replace with .WithMany(u => u.Posts) if User class have collection of posts
                  .HasForeignKey(p => p.AuthorID)
                  .HasConstraintName("FK_Posts_Users");
            entity.HasMany(p => p.BlogPosts)
                  .WithOne(bp => bp.Post)
                  .HasForeignKey(bp => bp.PostID)
                  .HasConstraintName("FK_BlogPosts_Posts");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.ToTable("BlogPosts", "Blogs");
            entity.HasKey(bp => new
                                {
                                    bp.BlogID, bp.PostID
                                });

            entity.HasOne(bp => bp.Blog)
                  .WithMany(b => b.BlogPosts)
                  .HasForeignKey(bp => bp.BlogID)
                  .HasConstraintName("FK_BlogPosts_Blogs");
            entity.HasOne(bp => bp.Post)
                  .WithMany(p => p.BlogPosts)
                  .HasForeignKey(bp => bp.PostID)
                  .HasConstraintName("FK_BlogPosts_Posts");
        });

        modelBuilder.Entity<PostType>(entity =>
        {
            entity.ToTable("lkPostTypes", "Blogs");
            entity.HasKey(e => e.PostTypeID);
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.WorldID);
            entity.HasMany(w => w.MetaData)
                  .WithOne(m => m.World)
                  .HasForeignKey(m => m.WorldID);
            entity.HasOne(w => w.CurrentWorldDate)
                  .WithOne(d => d.World)
                  .HasForeignKey<WorldDate>(d => d.WorldID)
                  .HasConstraintName("FK_WorldDates_Worlds");
        });

        modelBuilder.Entity<WorldDate>(entity =>
        {
            entity.HasKey(e => e.WorldID);
            entity.HasOne(d => d.World)
                  .WithOne(w => w.CurrentWorldDate)
                  .HasForeignKey<WorldDate>(d => d.WorldID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<WorldMeta>(entity =>
        {
            entity.HasKey(e => new { e.WorldID, e.MetaID });
            entity.HasOne(wm => wm.Template)
                  .WithMany()// assuming no navigation back from MetaTemplate
                  .HasForeignKey(wm => wm.MetaID)
                  .HasConstraintName("FK_WorldMetas_MetaTemplates");
        });

        modelBuilder.Entity<MetaTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateID);
            entity.Property(e => e.TemplateID).ValueGeneratedOnAdd();
            entity.Property(x => x.TemplateName).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(255);
            entity.Property(x => x.Placeholder).HasMaxLength(255);
            entity.Property(x => x.HelpText).HasMaxLength(255);
            entity.HasOne(mt => mt.Category)
                  .WithMany()// assuming no navigation back from MetaCategory
                  .HasForeignKey(mt => mt.CategoryID)
                  .HasConstraintName("FK_MetaTemplates_MetaCategories");
        });

        modelBuilder.Entity<MetaCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryID);
            entity.HasData(new List<MetaCategory>()
                           {
                               new MetaCategory
                               {
                                   CategoryID = 1,
                                   Description = "Goals"
                               },
                               new MetaCategory
                               {
                                   CategoryID = 2,
                                   Description = "Theme"
                               },
                               new MetaCategory
                               {
                                   CategoryID = 3,
                                   Description = "Focus"
                               },
                               new MetaCategory
                               {
                                   CategoryID = 4,
                                   Description = "Setting"
                               },
                               new MetaCategory
                               {
                                   CategoryID = 5,
                                   Description = "Residents"
                               },
                               new MetaCategory
                               {
                                   CategoryID = 6,
                                   Description = "Conflict"
                               },
                               new MetaCategory
                               {
                                   CategoryID = 7,
                                   Description = "Inspiration"
                               }
                           });
        });
    }
}