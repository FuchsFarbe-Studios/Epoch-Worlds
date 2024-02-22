// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Client;
using EpochApp.Shared.Config;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Data
{
    public class EpochDataDbContext : DbContext
    {

        public EpochDataDbContext(DbContextOptions<EpochDataDbContext> options) : base(options)
        {
        }

        public EpochDataDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; private set; } = @"Data Source=C:\myFolder\myAccessFile.accdb;";

        // Client
        public DbSet<ContactPoint> ContactPoints { get; set; }

        // Settings
        public DbSet<ClientSetting> ClientSettings { get; set; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSocial> UserSocials { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        // Lookups
        public DbSet<SocialMedia> SocialMediae { get; set; }
        public DbSet<PartOfSpeech> PartsOfSpeech { get; set; }
        public DbSet<DictionaryWord> DictionaryWords { get; set; }
        // Worlds
        public DbSet<World> Worlds { get; set; }
        public DbSet<WorldMeta> WorldMetas { get; set; }
        public DbSet<MetaCategory> MetaCategories { get; set; }
        public DbSet<MetaTemplate> MetaTemplates { get; set; }
        // Articles
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleSection> ArticleSections { get; set; }
        public DbSet<Phoneme> Phonemes { get; set; }
        public DbSet<Vowel> Vowels { get; set; }
        public DbSet<Consonant> Consonants { get; set; }
        // Blogs
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Post> Posts { get; set; }

        // Content generation

        public DbSet<BuilderContent> BuilderContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<ContactPoint>(entity =>
            {
                entity.ToTable("ContactPoints", "Client");
                entity.HasKey(e => e.ContactPointId);
                entity.Property(e => e.ContactPointId)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.UserName)
                      .HasMaxLength(64);
                entity.Property(e => e.Email)
                      .HasMaxLength(128);
                entity.Property(e => e.ContactType)
                      .HasConversion<string>();
            });

            modelBuilder.Entity<ClientSetting>(entity =>
            {
                entity.ToTable("ClientSettings", "Client");
                entity.HasKey(e => e.SettingId);
                entity.Property(e => e.SettingId).ValueGeneratedOnAdd();
                entity.Property(e => e.FieldName).HasMaxLength(50);
            });

            modelBuilder.HasDefaultSchema("Users");

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.ToTable("lkArticleCategories", "Lookups");
                entity.HasKey(a => a.CategoryID);
                entity.Property(a => a.CategoryID)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.Description)
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Articles", "Articles");
                entity.HasKey(a => a.ArticleId);
                entity.Property(a => a.ArticleId)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.Title).HasMaxLength(255);
                entity.HasOne(a => a.Category)
                      .WithMany()
                      .HasForeignKey(a => a.CategoryId)
                      .HasConstraintName("FK_Articles_ArticleCategories");
                entity.HasOne(a => a.Builder)
                      .WithOne()
                      .HasForeignKey<Article>(a => a.BuilderId)
                      .HasConstraintName("FK_Articles_BuilderContents");
                entity.HasOne(a => a.Author)
                      .WithMany(a => a.OwnedArticles)
                      .HasForeignKey(x => x.AuthorId)
                      .HasConstraintName("FK_Articles_Users");
                entity.HasOne(a => a.World)
                      .WithMany(w => w.WorldArticles)
                      .HasForeignKey(x => x.WorldId)
                      .HasConstraintName("FK_Articles_Worlds");
            });

            modelBuilder.Entity<ArticleSection>(entity =>
            {
                entity.ToTable("Sections", "Articles");
                entity.HasKey(a => new
                                   {
                                       ArticleID = a.ArticleId, a.SectionID
                                   });
                entity.Property(a => a.SectionID)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.Title).HasMaxLength(255);
                entity.HasOne(a => a.Article)
                      .WithMany(a => a.Sections)
                      .HasForeignKey(x => x.ArticleId)
                      .HasConstraintName("FK_ArticleSections_Articles");
            });

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
                new List<Role>
                {
                    new Role
                    {
                        RoleID = 1, Description = "DEFAULT"
                    },
                    new Role
                    {
                        RoleID = 2, Description = "ACTIVATED"
                    },
                    new Role
                    {
                        RoleID = 3, Description = "MODERATOR"
                    },
                    new Role
                    {
                        RoleID = 4, Description = "INTERNAL"
                    },
                    new Role
                    {
                        RoleID = 5, Description = "ADMIN"
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

            // Lookups

            modelBuilder.Entity<PartOfSpeech>(entity =>
            {
                entity.ToTable("lkPartOfSpeech", "Lookups");
                entity.HasKey(x => x.PartOfSpeechId);
                entity.Property(x => x.PartOfSpeechId)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.Description)
                      .HasMaxLength(50);
                entity.Property(x => x.Abbreviation)
                      .HasMaxLength(10);
            });

            modelBuilder.Entity<DictionaryWord>(entity =>
            {
                entity.ToTable("lkDictionaryWords", "Lookups");
                entity.HasKey(x => x.WordId);
                entity.Property(x => x.WordId)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.Translations)
                      .HasMaxLength(1000);
                entity.HasOne(x => x.PartOfSpeech)
                      .WithMany()
                      .HasForeignKey(x => x.PartOfSpeechId)
                      .HasConstraintName("FK_DictionaryWords_PartsOfSpeech");
                entity.Property(x => x.Category)
                      .HasConversion<string>();
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
                entity.Property(m => m.CategoryID).UseIdentityColumn(3, 4);
                entity.Property(m => m.Description).HasColumnName("Description");
                entity.Property(m => m.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.ToTable("lkSocialMediae", "Lookups");
                entity.HasKey(m => m.SocialID);
                entity.Property(m => m.SocialID).UseIdentityColumn(12, 12);
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

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.ToTable("BlogPosts", "Blogs");
                entity.HasKey(bp => new { bp.BlogID, bp.PostID });
                entity.HasOne(bp => bp.Post)
                      .WithMany(p => p.BlogPosts)
                      .HasForeignKey(bp => bp.PostID)
                      .HasConstraintName("FK_BlogPosts_Posts");
                entity.HasOne(bp => bp.Blog)
                      .WithMany(b => b.BlogPosts)
                      .HasForeignKey(bp => bp.BlogID)
                      .HasConstraintName("FK_BlogPosts_Blogs");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blogs", "Blogs");
                entity.HasKey(x => x.BlogID);
                entity.Property(x => x.BlogID)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.BlogType)
                      .HasConversion<string>();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts", "Blogs");
                entity.HasKey(x => x.PostID);
                entity.Property(x => x.PostID)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.PostType)
                      .HasConversion<string>();
            });

            // Worlds
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
                entity.HasData(new List<MetaCategory>
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

                // Content Generation

                modelBuilder.Entity<BuilderContent>(entity =>
                {
                    entity.HasKey(bc => bc.ContentID);

                    // Defines conversion from ContentType enum to string and back
                    entity.Property(bc => bc.ContentType)
                          .HasConversion<string>();

                    entity.HasOne(bc => bc.Author)
                          .WithMany()
                          .HasForeignKey(bc => bc.AuthorID)
                          .OnDelete(DeleteBehavior.Restrict);// Or DeleteBehavior.SetNull, Cascade etc. based on your needs.

                    entity.HasOne(bc => bc.World)
                          .WithMany()
                          .HasForeignKey(bc => bc.WorldID)
                          .OnDelete(DeleteBehavior.Restrict);// Or DeleteBehavior.SetNull, Cascade etc. based on your needs.
                });
            });
        }
    }
}