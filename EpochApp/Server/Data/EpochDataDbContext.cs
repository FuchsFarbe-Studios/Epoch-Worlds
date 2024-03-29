// EpochWorlds
// EpochDataDbContext.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

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
        public DbSet<BanTicket> BanList { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSocial> UserSocials { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionTier> SubTiers { get; set; }

        // Lookups
        public DbSet<ISOLanguage> Languages { get; set; }
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
        public DbSet<ArticleHeader> ArticleHeaders { get; set; }
        public DbSet<ArticleFooter> ArticleFooters { get; set; }
        public DbSet<ArticleSideBarContent> ArticleSideContents { get; set; }
        public DbSet<ArticleMeta> ArticleMetas { get; set; }
        public DbSet<ArticleSection> ArticleSections { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<Manuscript> Manuscripts { get; set; }
        public DbSet<ManuscriptChapter> Chapters { get; set; }
        public DbSet<ArticleTemplate> ArticleTemplates { get; set; }
        public DbSet<ArticleTemplateMeta> ArticleTemplateMetas { get; set; }
        public DbSet<ArticleTemplateSection> ArticleTemplateSections { get; set; }

        public DbSet<Phoneme> Phonemes { get; set; }
        public DbSet<Vowel> Vowels { get; set; }
        public DbSet<Consonant> Consonants { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // Content generation
        public DbSet<BuilderContent> BuilderContents { get; set; }

        // Social
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
        public DbSet<WorldTag> WorldTags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureClient(modelBuilder);
            ConfigureBlogs(modelBuilder);

            modelBuilder.Entity<BanTicket>(entity =>
            {
                entity.ToTable("Blacklist", "Users");
                entity.HasKey(e => e.TicketId);
                entity.Property(x => x.Reason).HasMaxLength(2000);
                entity.HasOne(d => d.Admin)
                      .WithMany(p => p.AdminTickets)
                      .HasForeignKey(d => d.AdminID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_BanTicket_Admin");
                entity.HasOne(d => d.User)
                      .WithMany(p => p.UserTickets)
                      .HasForeignKey(d => d.UserID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_BanTicket_User");
            });

            modelBuilder.Entity<UserReport>(entity =>
            {
                entity.ToTable("GriefReports", "Users");
                entity.HasKey(e => new { e.PlaintiffId, e.DefendantId, e.OverseerId });
                entity.Property(x => x.Details).HasMaxLength(2000);
                entity.HasOne(d => d.Overseer)
                      .WithMany(p => p.AdminReports)
                      .HasForeignKey(d => d.OverseerId)
                      .HasConstraintName("FK_UserReports_Overseer");
                entity.HasOne(d => d.Plaintiff)
                      .WithMany(p => p.PlaintiffReports)
                      .HasForeignKey(d => d.PlaintiffId)
                      .HasConstraintName("FK_UserReports_Plaintiff");
                entity.HasOne(d => d.Defendant)
                      .WithMany(p => p.DefendantReports)
                      .HasForeignKey(d => d.DefendantId)
                      .HasConstraintName("FK_UserReports_Defendant");
            });

            modelBuilder.Entity<LoginAttempt>(entity =>
            {
                entity.ToTable("LoginAttempts", "Users");
                entity.HasKey(e => e.LoginAttempId);
                entity.Property(e => e.LoginAttempId).ValueGeneratedOnAdd();
                entity.Property(x => x.Browser).HasMaxLength(255);
                entity.Property(x => x.Device).HasMaxLength(255);
                entity.Property(x => x.Location).HasMaxLength(255);
                entity.Property(x => x.UserAgent).HasMaxLength(255);
                entity.Property(x => x.UsernameOrEmail).HasMaxLength(255);
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.ToTable("lkArticleCategories", "Lookups");
                entity.HasKey(a => a.CategoryID);
                entity.Property(a => a.CategoryID)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.Description)
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<ArticleTemplate>(entity =>
            {
                entity.ToTable("ArticleTemplates", "Templates");
                entity.HasKey(a => a.TemplateId);
                entity.Property(a => a.TemplateId)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.TemplateName).HasMaxLength(100).IsUnicode();
                entity.Property(a => a.Description).HasMaxLength(500).IsUnicode();
                entity.HasOne(a => a.Category)
                      .WithMany()
                      .HasForeignKey(a => a.CategoryId)
                      .HasConstraintName("FK_ArticleTemplates_ArticleCategories");
            });

            modelBuilder.Entity<ArticleTemplateMeta>(entity =>
            {
                entity.ToTable("ArticleTemplateMetaData", "Templates");
                entity.HasKey(a => new { a.TemplateId, a.MetaName });
                entity.Property(a => a.MetaName).HasMaxLength(100).IsUnicode();
                entity.Property(a => a.Description).HasMaxLength(500).IsUnicode();
                entity.Property(a => a.Placeholder).HasMaxLength(255).IsUnicode();
                entity.Property(a => a.HelpText).HasMaxLength(255).IsUnicode();
                entity.Property(e => e.Type).HasConversion<string>();
                entity.HasOne(a => a.Template)
                      .WithMany(t => t.Meta)
                      .HasForeignKey(a => a.TemplateId)
                      .HasConstraintName("FK_ArticleTemplateMetas_ArticleTemplates")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ArticleTemplateSection>(entity =>
            {
                entity.ToTable("ArticleTemplateSections", "Templates");
                entity.HasKey(a => new { a.TemplateId, a.SectionName });
                entity.Property(a => a.SectionName).HasMaxLength(100).IsUnicode();
                entity.Property(a => a.Description).HasMaxLength(500).IsUnicode();
                entity.Property(a => a.Placeholder).HasMaxLength(255).IsUnicode();
                entity.Property(a => a.HelpText).HasMaxLength(255).IsUnicode();
                entity.HasOne(a => a.Template)
                      .WithMany(t => t.Sections)
                      .HasForeignKey(a => a.TemplateId)
                      .HasConstraintName("FK_ArticleTemplateSections_ArticleTemplates")
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Manuscript>(entity =>
            {
                entity.ToTable("Manuscripts", "Articles");
                entity.HasKey(m => new { m.UserId, m.ManuscriptId });
                entity.Property(m => m.ManuscriptId)
                      .ValueGeneratedOnAdd();
                entity.Property(m => m.Title).HasMaxLength(255);
                entity.Property(m => m.Summary)
                      .HasMaxLength(10000)
                      .IsUnicode();
                entity.Property(m => m.CoverArt)
                      .HasMaxLength(500);
                entity.HasOne(d => d.User)
                      .WithMany(p => p.Manuscripts)
                      .HasForeignKey(d => d.UserId)
                      .HasConstraintName("FK_Manuscripts_Users")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Navigation(x => x.Chapters)
                      .AutoInclude();
            });

            modelBuilder.Entity<ManuscriptChapter>(entity =>
            {
                entity.ToTable("Chapters", "Articles");
                entity.HasKey(m => new { m.UserId, m.ManuscriptId, m.ChapterId });
                entity.Property(m => m.ChapterId)
                      .ValueGeneratedOnAdd();
                entity.Property(m => m.Title).HasMaxLength(255);
                entity.HasOne(d => d.Manuscript)
                      .WithMany(p => p.Chapters)
                      .HasForeignKey(d => new { d.UserId, d.ManuscriptId })
                      .HasConstraintName("FK_Chapters_Manuscripts")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ArticleSideBarContent>(entity =>
            {
                entity.ToTable("ArticleSideBars", "Articles");
                entity.HasKey(a => a.ArticleId);
                entity.Property(x => x.ArticleId).HasColumnName("ArticleId");
                entity.HasOne(a => a.Article)
                      .WithOne(a => a.SideBar)
                      .HasForeignKey<ArticleSideBarContent>(a => a.ArticleId)
                      .HasConstraintName("FK_ArticleSideBarContents_Articles")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(x => x.SideBarBottom).HasMaxLength(1000);
                entity.Property(x => x.SideBarTop).HasMaxLength(1000);
                entity.Property(x => x.SideBarBottomContent).HasMaxLength(1000);
                entity.Property(x => x.SideBarTopContent).HasMaxLength(1000);
            });

            modelBuilder.Entity<ArticleHeader>(entity =>
            {
                entity.ToTable("ArticleHeaders", "Articles");
                entity.HasKey(a => a.ArticleId);// Assuming one-to-one relationship
                entity.Property(x => x.ArticleId).HasColumnName("ArticleId");
                entity.HasOne(a => a.Article)
                      .WithOne(a => a.Header)
                      .HasForeignKey<ArticleHeader>(a => a.ArticleId)
                      .HasConstraintName("FK_ArticleHeaders_Articles");
                entity.Property(x => x.SubHeading).HasMaxLength(255);
                entity.Property(x => x.Credits).HasMaxLength(2000);
            });

            modelBuilder.Entity<ArticleFooter>(entity =>
            {
                entity.ToTable("ArticleFooters", "Articles");
                entity.HasKey(a => a.ArticleId);
                entity.Property(x => x.ArticleId).HasColumnName("ArticleId");
                entity.HasOne(a => a.Article)
                      .WithOne(a => a.Footer)
                      .HasForeignKey<ArticleFooter>(a => a.ArticleId)
                      .HasConstraintName("FK_ArticleFooters_Articles");
                entity.Property(x => x.Footnotes).HasMaxLength(1000);
                entity.Property(x => x.FooterContent).HasMaxLength(2000);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Articles", "Articles");
                entity.HasKey(a => a.ArticleId);
                entity.Property(a => a.ArticleId)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.Title).HasMaxLength(255);
                entity.Property(a => a.Excerpt).HasMaxLength(500);
                entity.Property(a => a.Icon).HasMaxLength(50);
                entity.Property(a => a.CoverImage).HasMaxLength(255);
                entity.Property(a => a.CoverImageAlt).HasMaxLength(500);
                entity.Property(a => a.MouseOverSnippet).HasMaxLength(500);
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
                entity.HasMany(a => a.SubArticles)
                      .WithOne(a => a.ParentArticle)
                      .HasForeignKey(a => a.ParentArticleId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(x => x.Slug)
                      .HasMaxLength(255);

                entity.HasQueryFilter(x => x.DeletedOn == null || x.DeletedOn > DateTime.UtcNow);
                entity.Navigation(x => x.Sections).AutoInclude();
                entity.Navigation(x => x.Header).AutoInclude();
                entity.Navigation(x => x.Footer).AutoInclude();
                entity.Navigation(x => x.SideBar).AutoInclude();
            });

            modelBuilder.Entity<ArticleMeta>(entity =>
            {
                entity.ToTable("ArticleMetaData", "Articles");
                entity.HasKey(e => new { e.ArticleId, e.MetaId, e.MetaField });// For composite key
                entity.Property(e => e.MetaField).HasMaxLength(500);
                entity.Property(e => e.MetaValue).HasMaxLength(500);
                entity.Property(x => x.MetaField)
                      .HasConversion<string>();

                // Configure relationships
                entity.HasOne(d => d.Article)
                      .WithMany(p => p.Meta)
                      .HasForeignKey(d => d.ArticleId)
                      .HasConstraintName("FK_ArticleMetaData_Articles")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ArticleSection>(entity =>
            {
                entity.ToTable("Sections", "Articles");
                entity.HasKey(a => new { ArticleID = a.ArticleId, a.SectionID });
                entity.Property(a => a.SectionID)
                      .ValueGeneratedOnAdd();
                entity.Property(a => a.Title).HasMaxLength(255);
                entity.HasOne(a => a.Article)
                      .WithMany(a => a.Sections)
                      .HasForeignKey(x => x.ArticleId)
                      .HasConstraintName("FK_ArticleSections_Articles");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscriptions", "Users");
                entity.HasKey(x => new { x.UserId, x.SubscriptionId });
                entity.Property(x => x.SubscriptionId)
                      .UseIdentityColumn(10000);
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Subscriptions)
                      .HasForeignKey(s => s.UserId)
                      .HasForeignKey("FK_Subscriptions_Users")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Tier)
                      .WithMany(t => t.Subscriptions)
                      .HasForeignKey(s => s.TierId)
                      .HasConstraintName("FK_Subscriptions_SubscriptionTiers");
            });

            modelBuilder.Entity<SubscriptionTier>(entity =>
            {
                entity.ToTable("Tiers", "Users");
                entity.HasKey(x => x.TierId);
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.Icon).HasMaxLength(100);
                entity.Property(x => x.IconAlt).HasMaxLength(500);
                entity.Property(x => x.Price).HasPrecision(18, 2);
                entity.Property(x => x.Image).HasMaxLength(255);
                entity.Property(x => x.ImageAlt).HasMaxLength(500);
                entity.Property(x => x.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UserCategory>(entity =>
            {
                entity.ToTable("UserCategories", "Users");
                entity.HasKey(x => x.CategoryId);
                entity.Property(x => x.CategoryName).HasMaxLength(100);
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.Icon).HasMaxLength(100);
                entity.Property(x => x.IconAlt).HasMaxLength(300);
                entity.HasOne(x => x.ParentCategory)
                      .WithMany(x => x.ChildCategories)
                      .HasForeignKey(x => x.ParentId)
                      .HasConstraintName("FK_UserCategories_Categories")
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.User)
                      .WithMany()
                      .HasForeignKey(x => x.UserId)
                      .HasConstraintName("FK_UserCategories_Users")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users", "Users");
                user.Property(p => p.UserName)
                    .HasMaxLength(128);
                user.Property(p => p.Email)
                    .HasMaxLength(255);
                user.Ignore(x => x.NormalizedUserName);
                user.Ignore(x => x.NormalizedEmail);
                user.HasMany(u => u.OwnedWorlds)
                    .WithOne(w => w.Owner)
                    .HasForeignKey(w => w.OwnerId)
                    .HasConstraintName("FK_Worlds_Users")
                    .OnDelete(DeleteBehavior.Cascade);
                user.HasQueryFilter(x => x.DateRemoved == null || x.DateRemoved > DateTime.Now);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles", "Users");
                entity.HasKey(ur => new { ur.RoleID, ur.UserID });
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserID)
                      .HasConstraintName("FK_UserRoles_Users")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(ur => ur.RoleID)
                      .HasConstraintName("FK_UserRoles_Roles");
                entity.HasQueryFilter(x => x.DateRemoved == null || x.DateRemoved > DateTime.Now);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "Users");
                entity.HasData(
                new List<Role>
                {
                    new Role
                    {
                        RoleID = 1, Description = "DEFAULT"
                    },
                    new Role
                    {
                        RoleID = 2, Description = "VERIFIED"
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
                us.ToTable("UserSocials", "Users");
                us.HasKey(x => new { x.SocialID, x.UserID });
                us.Property(x => x.SocialID).ValueGeneratedOnAdd();
                us.Property(x => x.SocialHandle).HasMaxLength(255);
                us.HasOne(x => x.Profile)
                  .WithMany(p => p.Socials)
                  .HasForeignKey(x => x.UserID)
                  .HasConstraintName("FK_UserSocials_Users")
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles", "Users");
                entity.HasKey(p => p.UserID);
                entity.Property(x => x.Signature).HasMaxLength(300);
                entity.Property(x => x.CommunitySignature).HasMaxLength(300);
                entity.Property(x => x.AvatarImg).HasMaxLength(255);
                entity.Property(x => x.AvatarImgAlt).HasMaxLength(500);
                entity.Property(x => x.CoverImg).HasMaxLength(255);
                entity.Property(x => x.CoverImgAlt).HasMaxLength(500);
                entity.Property(x => x.FirstName).HasMaxLength(255);
                entity.Property(x => x.LastName).HasMaxLength(255);
                entity.Property(x => x.WebAddress).HasMaxLength(255);
                entity.HasOne(p => p.User)
                      .WithOne(u => u.Profile)
                      .HasForeignKey<Profile>(p => p.UserID)
                      .HasConstraintName("FK_Profile_User")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            ConfigureLookups(modelBuilder);

            // Worlds
            modelBuilder.Entity<World>(entity =>
            {
                entity.ToTable("Worlds", "Worlds");
                entity.HasKey(e => e.WorldId);
                entity.HasMany(w => w.MetaData)
                      .WithOne(m => m.World)
                      .HasForeignKey(m => m.WorldId);
                entity.HasOne(w => w.CurrentWorldDate)
                      .WithOne(d => d.World)
                      .HasForeignKey<WorldDate>(d => d.WorldID)
                      .HasConstraintName("FK_WorldDates_Worlds");
                entity.Navigation(x => x.CurrentWorldDate)
                      .AutoInclude();
                entity.Navigation(x => x.WorldTags)
                      .AutoInclude();
                entity.Property(x => x.Slug)
                      .HasMaxLength(255);
                entity.HasQueryFilter(x => x.RemovedOn == null || x.RemovedOn > DateTime.UtcNow);
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
                      .HasConstraintName("FK_WorldGenres_Genres")
                      .OnDelete(DeleteBehavior.ClientNoAction);
            });

            modelBuilder.Entity<WorldDate>(entity =>
            {
                entity.ToTable("WorldDates", "Worlds");
                entity.HasKey(e => e.WorldID);
                entity.HasOne(d => d.World)
                      .WithOne(w => w.CurrentWorldDate)
                      .HasForeignKey<WorldDate>(d => d.WorldID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorldMeta>(entity =>
            {
                entity.ToTable("WorldMetas", "Worlds");
                entity.HasKey(e => new { e.WorldId, e.MetaID });
                entity.HasOne(wm => wm.Template)
                      .WithMany()// assuming no navigation back from MetaTemplate
                      .HasForeignKey(wm => wm.MetaID)
                      .HasConstraintName("FK_WorldMetas_MetaTemplates");
                entity.HasQueryFilter(x => x.DateRemoved == null || x.DateRemoved > DateTime.UtcNow);
            });

            modelBuilder.Entity<MetaTemplate>(entity =>
            {
                entity.ToTable("MetaTemplates", "Templates");
                entity.HasKey(e => e.TemplateId);
                entity.Property(e => e.TemplateId).ValueGeneratedOnAdd();
                entity.Property(x => x.TemplateName).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(255);
                entity.Property(x => x.Placeholder).HasMaxLength(255);
                entity.Property(x => x.HelpText).HasMaxLength(255);
                entity.HasOne(mt => mt.Category)
                      .WithMany(mc => mc.Templates)// assuming no navigation back from MetaCategory
                      .HasForeignKey(mt => mt.CategoryId)
                      .HasConstraintName("FK_MetaTemplates_MetaCategories");
            });

            modelBuilder.Entity<MetaCategory>(entity =>
            {
                entity.ToTable("MetaCategories", "Templates");
                entity.HasKey(e => e.CategoryId);
                entity.HasData(new List<MetaCategory>
                               {
                                   new MetaCategory
                                   {
                                       CategoryId = 1,
                                       Description = "Goals"
                                   },
                                   new MetaCategory
                                   {
                                       CategoryId = 2,
                                       Description = "Theme"
                                   },
                                   new MetaCategory
                                   {
                                       CategoryId = 3,
                                       Description = "Focus"
                                   },
                                   new MetaCategory
                                   {
                                       CategoryId = 4,
                                       Description = "Setting"
                                   },
                                   new MetaCategory
                                   {
                                       CategoryId = 5,
                                       Description = "Residents"
                                   },
                                   new MetaCategory
                                   {
                                       CategoryId = 6,
                                       Description = "Conflict"
                                   },
                                   new MetaCategory
                                   {
                                       CategoryId = 7,
                                       Description = "Inspiration"
                                   }
                               });

                // Content Generation

                modelBuilder.Entity<BuilderContent>(builder =>
                {
                    builder.ToTable("BuilderContents", "Users");
                    builder.HasKey(bc => bc.ContentID);

                    // Defines conversion from ContentType enum to string and back
                    builder.Property(bc => bc.ContentType)
                           .HasConversion<string>();

                    builder.HasOne(bc => bc.Author)
                           .WithMany()
                           .HasForeignKey(bc => bc.AuthorID)
                           .OnDelete(DeleteBehavior.Restrict);// Or DeleteBehavior.SetNull, Cascade etc. based on your needs.

                    builder.HasOne(bc => bc.World)
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

        private void ConfigureBlogs(ModelBuilder builder)
        {
            builder.Entity<PostTag>(entity =>
            {
                entity.ToTable("PostTags", "Blogs");
                entity.HasKey(e => new { e.PostId, e.TagId });
                entity.HasOne(pt => pt.Post)
                      .WithMany(p => p.PostTags)
                      .HasForeignKey(t => t.PostId)
                      .HasConstraintName("FK_PostTags_Posts")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(pt => pt.Tag)
                      .WithMany(p => p.PostTags)
                      .HasForeignKey(t => t.TagId)
                      .HasConstraintName("FK_PostTags_Tags");
            });

            builder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts", "Blogs");
                entity.HasKey(x => x.PostId);
                entity.Property(x => x.PostId)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.Title).HasMaxLength(255);
                entity.Property(x => x.Content).HasMaxLength(10000);
                entity.Property(x => x.Image).HasMaxLength(255);
                entity.Property(x => x.ImageAlt).HasMaxLength(500);
                entity.Property(x => x.ExternalLink).HasMaxLength(255);
                entity.Property(x => x.Views).HasDefaultValue(0);
                entity.Property(x => x.IsPublished).HasDefaultValue(false);
                entity.Property(x => x.Type).HasConversion<string>();
                entity.HasOne(x => x.Blog)
                      .WithMany(x => x.Posts)
                      .HasForeignKey(x => x.BlogId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Posts_Blogs");
            });

            builder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blogs", "Blogs");
                entity.HasKey(x => x.BlogId);
                entity.Property(x => x.BlogId)
                      .ValueGeneratedOnAdd();
                entity.Property(x => x.BlogName).HasMaxLength(50);
                entity.Property(x => x.Description).HasMaxLength(500);
            });
        }

        private void ConfigureClient(ModelBuilder modelBuilder)
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
                entity.HasQueryFilter(x => x.ResolvedOn == null || x.ResolvedOn > DateTime.Now);
            });

            modelBuilder.Entity<ClientSetting>(entity =>
            {
                entity.ToTable("ClientSettings", "Client");
                entity.HasKey(e => e.SettingId);
                entity.Property(e => e.SettingId)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.FieldName).HasMaxLength(50);
                entity.Property(e => e.SettingField).HasMaxLength(100);
                entity.Property(e => e.SettingValue).HasMaxLength(500);

                var data = new List<ClientSetting>
                           {
                               new ClientSetting
                               {
                                   SettingId = 12,
                                   FieldName = "Company",
                                   SettingField = "Name",
                                   SettingValue = "FuchsFarbe Studios LLC",
                                   SettingFieldId = 0
                               },
                               new ClientSetting
                               {
                                   SettingId = 13,
                                   FieldName = "Company",
                                   SettingField = "Address",
                                   SettingValue = "4078 Laurel Lane, Mount Joy PA, 17552",
                                   SettingFieldId = 0
                               },
                               new ClientSetting
                               {
                                   SettingId = 14,
                                   FieldName = "Company",
                                   SettingField = "Phone",
                                   SettingValue = "(717) 824-7924",
                                   SettingFieldId = 0
                               },
                               new ClientSetting
                               {
                                   SettingId = 15,
                                   FieldName = "Company",
                                   SettingField = "SupportEmail",
                                   SettingValue = "contact@epochgen.com",
                                   SettingFieldId = 0
                               },
                               new ClientSetting
                               {
                                   SettingId = 16,
                                   FieldName = "Company",
                                   SettingField = "SiteName",
                                   SettingValue = "The Epoch Exchange",
                                   SettingFieldId = 0
                               },
                               new ClientSetting
                               {
                                   SettingId = 17,
                                   FieldName = "Company",
                                   SettingField = "ContactLink",
                                   SettingValue = "https://epochgen.com/Contact",
                                   SettingFieldId = 0
                               }
                           };
                entity.HasData(data);
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
                entity.HasQueryFilter(x => x.RemovedOn == null || x.RemovedOn > DateTime.Now);
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
                entity.HasKey(e => e.TemplateId);
                entity.Property(e => e.TemplateId).ValueGeneratedOnAdd();
                entity.Property(e => e.TemplateName).HasColumnName("Name");
                entity.Property(e => e.TemplateName).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.HelpText).HasMaxLength(150);
                entity.Property(e => e.Placeholder).HasMaxLength(150);
                entity.HasOne(e => e.Category)
                      .WithMany(e => e.Templates)
                      .HasForeignKey(e => e.CategoryId)
                      .HasConstraintName("FK_MetaTemplates_MetaCategories")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MetaCategory>(entity =>
            {
                entity.ToTable("lkMetaCategories", "Lookups");
                entity.HasKey(m => m.CategoryId);
                entity.Property(m => m.CategoryId)
                      .ValueGeneratedOnAdd();
                entity.Property(m => m.Description)
                      .HasColumnName("Description");
                entity.Property(m => m.Description)
                      .HasMaxLength(50);
                // entity.Navigation(x=>x.Templates)
                //       .AutoInclude();
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
                entity.HasKey(e => e.PhonemeId);
                entity.Property(e => e.PhonemeChar)
                      .HasMaxLength(10)
                      .IsUnicode()
                      .ValueGeneratedNever();
                entity.Property(e => e.AudioFile)
                      .HasMaxLength(155);
            });

            modelBuilder.Entity<Consonant>(entity =>
            {
                entity.ToTable("lkConsonants", "Lookups");
                entity.Property(e => e.PhonemeId).UseIdentityColumn(2, 2);
                entity.Property(e => e.Manner).HasConversion<string>();
                entity.Property(e => e.Place).HasConversion<string>();
            });

            modelBuilder.Entity<Vowel>(entity =>
            {
                entity.ToTable("lkVowels", "Lookups");
                entity.Property(e => e.PhonemeId).UseIdentityColumn(1, 2);
                entity.Property(e => e.Depth).HasConversion<string>();
                entity.Property(e => e.Verticality).HasConversion<string>();
            });
        }
    }
}