// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Client;
using EpochApp.Shared.Config;
using EpochApp.Shared.Social;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Data
{
    /// <summary>
    ///     The main database context for the application.
    /// </summary>
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
        public DbSet<ClientSetting> ClientSettings { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSocial> UserSocials { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }

        // Lookups
        public DbSet<ISOLanguage> Languages { get; set; }
        public DbSet<SocialMedia> SocialMediae { get; set; }
        public DbSet<PartOfSpeech> PartsOfSpeech { get; set; }
        public DbSet<DictionaryWord> DictionaryWords { get; set; }

        // Worlds
        public DbSet<World> Worlds { get; set; }
        public DbSet<WorldGenre> WorldGenres { get; set; }
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
        public DbSet<Genre> Genres { get; set; }

        // Blogs
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Post> Posts { get; set; }

        // Content generation

        public DbSet<BuilderContent> BuilderContents { get; set; }

        // Social
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
        public DbSet<WorldTag> WorldTags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureClient(modelBuilder);

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

            ConfigureLookups(modelBuilder);

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
                entity.ToTable("Worlds", "Worlds");
                entity.HasKey(e => e.WorldID);
                entity.HasMany(w => w.MetaData)
                      .WithOne(m => m.World)
                      .HasForeignKey(m => m.WorldID);
                entity.HasOne(w => w.CurrentWorldDate)
                      .WithOne(d => d.World)
                      .HasForeignKey<WorldDate>(d => d.WorldID)
                      .HasConstraintName("FK_WorldDates_Worlds");
                entity.Navigation(x => x.CurrentWorldDate)
                      .AutoInclude();
                entity.Navigation(x => x.WorldTags)
                      .AutoInclude();
            });

            modelBuilder.Entity<WorldGenre>(entity =>
            {
                entity.ToTable("WorldGenres", "Worlds");
                entity.HasKey(e => new { e.GenreID, e.WorldID });
                entity.HasOne(d => d.World)
                      .WithMany(w => w.WorldGenres)
                      .HasForeignKey(x => x.WorldID)
                      .HasConstraintName("FK_WorldGenres_Worlds")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(d => d.Genre)
                      .WithMany()
                      .HasForeignKey(x => x.GenreID)
                      .HasConstraintName("FK_WorldGenres_Genres");
            });

            modelBuilder.Entity<WorldDate>(entity =>
            {
                entity.ToTable("WorldDates", "Worlds");
                entity.HasKey(e => e.WorldID);
                entity.HasOne(d => d.World)
                      .WithOne(w => w.CurrentWorldDate)
                      .HasForeignKey<WorldDate>(d => d.WorldID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WorldMeta>(entity =>
            {
                entity.ToTable("WorldMetas", "Worlds");
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

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tags", "Social");
                entity.HasKey(e => e.TagId);
                entity.Property(e => e.TagId)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.Text)
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<UserTag>(entity =>
            {
                entity.ToTable("UserTags", "Social");
                entity.HasKey(e => new { e.UserId, e.TagId });
                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.UserTags)
                      .HasForeignKey(ut => ut.UserId)
                      .HasConstraintName("FK_UserTags_Users");
                entity.HasOne(ut => ut.Tag)
                      .WithMany(t => t.UserTags)
                      .HasForeignKey(ut => ut.TagId)
                      .HasConstraintName("FK_UserTags_Tags");
            });

            modelBuilder.Entity<WorldTag>(entity =>
            {
                entity.ToTable("WorldTags", "Social");
                entity.HasKey(e => new { e.WorldId, e.TagId });
                entity.HasOne(wt => wt.World)
                      .WithMany(w => w.WorldTags)
                      .HasForeignKey(wt => wt.WorldId)
                      .HasConstraintName("FK_WorldTags_Worlds");
                entity.HasOne(wt => wt.Tag)
                      .WithMany(t => t.WorldTags)
                      .HasForeignKey(wt => wt.TagId)
                      .HasConstraintName("FK_WorldTags_Tags");
            });

            modelBuilder.Entity<ArticleTag>(entity =>
            {
                entity.ToTable("ArticleTags", "Social");
                entity.HasKey(e => new { e.ArticleId, e.TagId });
                entity.HasOne(at => at.Article)
                      .WithMany(a => a.ArticleTags)
                      .HasForeignKey(at => at.ArticleId)
                      .HasConstraintName("FK_ArticleTags_Articles");
                entity.HasOne(at => at.Tag)
                      .WithMany(t => t.ArticleTags)
                      .HasForeignKey(at => at.TagId)
                      .HasConstraintName("FK_ArticleTags_Tags");
            });
        }

        private static void ConfigureClient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.ToTable("EmailTemplates", "Client");
                entity.HasKey(e => e.TemplateId);
                entity.Property(e => e.TemplateId)
                      .HasConversion<string>();
                entity.Property(e => e.Subject)
                      .HasMaxLength(255);
                var templates = Enum.GetValues<EmailTemplateType>()
                                    .Select(type => new EmailTemplate
                                                    {
                                                        TemplateId = type,
                                                        Subject = "",
                                                        HtmlBody = ""

                                                    })
                                    .ToList();
                entity.HasData(templates);
            });

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

            modelBuilder.Entity<UserFile>(entity =>
            {
                entity.ToTable("Files", "Client");
                entity.HasKey(e => e.FileId);
                entity.Property(e => e.FileId)
                      .ValueGeneratedOnAdd();
                entity.HasOne<User>(u => u.User)
                      .WithMany(uf => uf.UserFiles)
                      .HasForeignKey(u => u.UserId)
                      .HasConstraintName("FK_UserFiles_Users");
                entity.HasOne<World>(w => w.World)
                      .WithMany(uf => uf.WorldFiles)
                      .HasForeignKey(w => w.WorldId)
                      .HasConstraintName("FK_WorldFiles_Worlds");
            });
        }

        private void ConfigureLookups(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("lkGenres", "Lookups");
                entity.HasKey(x => x.GenreId);
                entity.Property(x => x.GenreId)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.GenreName)
                      .HasMaxLength(50);
                entity.Property(x => x.Description)
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<ISOLanguage>(entity =>
            {
                entity.ToTable("lkLanguage", "Lookups");
                entity.HasKey(x => x.LanguageCode);
                entity.Property(x => x.LanguageCode).HasMaxLength(3);
                entity.Property(x => x.LanguageName).HasMaxLength(100);
                entity.Property(x => x.Set2T).HasMaxLength(5);
                entity.Property(x => x.Set3).HasMaxLength(5);
                entity.Property(x => x.Notes).HasMaxLength(500);
            });

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
                entity.Property(e => e.PhonemeID)
                      .HasMaxLength(4)
                      .ValueGeneratedNever();
                entity.Property(e => e.AudioFile)
                      .HasMaxLength(155);
            });

            modelBuilder.Entity<Consonant>(entity =>
            {
                entity.ToTable("lkConsonants", "Lookups");
                entity.Property(e => e.Manner).HasConversion<string>();
                entity.Property(e => e.Place).HasConversion<string>();
            });

            modelBuilder.Entity<Vowel>(entity =>
            {
                entity.ToTable("lkVowels", "Lookups");
                entity.Property(e => e.Depth).HasConversion<string>();
                entity.Property(e => e.Verticality).HasConversion<string>();
            });
        }
    }
}