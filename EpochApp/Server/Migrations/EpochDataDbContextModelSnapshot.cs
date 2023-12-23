﻿// <auto-generated />
using System;
using EntityFrameworkCore.Jet.Metadata;
using EpochApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpochApp.Server.Migrations
{
    [DbContext(typeof(EpochDataDbContext))]
    partial class EpochDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Users")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EpochApp.Shared.Blog", b =>
                {
                    b.Property<int>("BlogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogID"));

                    b.Property<int>("BlogTypeID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogID");

                    b.HasIndex("BlogTypeID");

                    b.ToTable("Blogs", "Blogs");
                });

            modelBuilder.Entity("EpochApp.Shared.BlogOwner", b =>
                {
                    b.Property<int>("BlogID")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RemovedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("BlogID", "OwnerID");

                    b.HasIndex("OwnerID");

                    b.ToTable("BlogOwners", "Blogs");
                });

            modelBuilder.Entity("EpochApp.Shared.BlogPost", b =>
                {
                    b.Property<int>("BlogID")
                        .HasColumnType("int");

                    b.Property<Guid>("PostID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PostedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("BlogID", "PostID");

                    b.HasIndex("PostID");

                    b.ToTable("BlogPosts", "Blogs");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.ArticleCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Jet:IdentityIncrement", 4)
                        .HasAnnotation("Jet:IdentitySeed", 3)
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Description");

                    b.HasKey("CategoryID");

                    b.ToTable("lkArticleCategories", "Lookups");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.BlogType", b =>
                {
                    b.Property<int>("BlogTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogTypeID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogTypeID");

                    b.ToTable("lkBlogTypes", "Blogs");

                    b.HasData(
                        new
                        {
                            BlogTypeID = 1,
                            Description = "NEWS"
                        },
                        new
                        {
                            BlogTypeID = 2,
                            Description = "UPDATES"
                        },
                        new
                        {
                            BlogTypeID = 3,
                            Description = "EVENTS"
                        },
                        new
                        {
                            BlogTypeID = 4,
                            Description = "FAQ"
                        },
                        new
                        {
                            BlogTypeID = 5,
                            Description = "DOCUMENTATION"
                        });
                });

            modelBuilder.Entity("EpochApp.Shared.Config.MetaCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Description");

                    b.HasKey("CategoryID");

                    b.ToTable("lkMetaCategories", "Lookups");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            Description = "Goals"
                        },
                        new
                        {
                            CategoryID = 2,
                            Description = "Theme"
                        },
                        new
                        {
                            CategoryID = 3,
                            Description = "Focus"
                        },
                        new
                        {
                            CategoryID = 4,
                            Description = "Setting"
                        },
                        new
                        {
                            CategoryID = 5,
                            Description = "Residents"
                        },
                        new
                        {
                            CategoryID = 6,
                            Description = "Conflict"
                        },
                        new
                        {
                            CategoryID = 7,
                            Description = "Inspiration"
                        });
                });

            modelBuilder.Entity("EpochApp.Shared.Config.MetaTemplate", b =>
                {
                    b.Property<int>("TemplateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TemplateID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HelpText")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Placeholder")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TemplateName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("TemplateID");

                    b.HasIndex("CategoryID");

                    b.ToTable("lkMetaTemplates", "Lookups");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.Phoneme", b =>
                {
                    b.Property<string>("PhonemeID")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("AudioFile")
                        .HasMaxLength(155)
                        .HasColumnType("nvarchar(155)");

                    b.HasKey("PhonemeID");

                    b.ToTable("lkPhonemes", "Lookups");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("EpochApp.Shared.Config.PostType", b =>
                {
                    b.Property<int>("PostTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostTypeID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostTypeID");

                    b.ToTable("lkPostTypes", "Blogs");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.SocialMedia", b =>
                {
                    b.Property<int>("SocialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Jet:IdentityIncrement", 12)
                        .HasAnnotation("Jet:IdentitySeed", 12)
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialID"));

                    b.Property<string>("Icon")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SocialMediaName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Description");

                    b.Property<string>("URLAffix")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("SocialID");

                    b.ToTable("lkSocialMediae", "Lookups");
                });

            modelBuilder.Entity("EpochApp.Shared.Post", b =>
                {
                    b.Property<Guid>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Href")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("OutsideLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostTypeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("PostTypeID");

                    b.ToTable("Posts", "Blogs");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.Profile", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Profiles", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles", "Users");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Description = "DEFAULT"
                        },
                        new
                        {
                            RoleID = 2,
                            Description = "ACTIVATED"
                        },
                        new
                        {
                            RoleID = 3,
                            Description = "MODERATOR"
                        },
                        new
                        {
                            RoleID = 4,
                            Description = "INTERNAL"
                        },
                        new
                        {
                            RoleID = 5,
                            Description = "ADMIN"
                        });
                });

            modelBuilder.Entity("EpochApp.Shared.Users.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRemoved")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("UserID");

                    b.ToTable("Users", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.UserRole", b =>
                {
                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAssigned")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRemoved")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserRoles", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.UserSocial", b =>
                {
                    b.Property<int>("SocialID")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SocialHandle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserSocials", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.World", b =>
                {
                    b.Property<Guid>("WorldID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRemoved")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Pronunciation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorldName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorldID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Worlds", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.WorldDate", b =>
                {
                    b.Property<Guid>("WorldID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurrentAge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentDay")
                        .HasColumnType("int");

                    b.Property<int>("CurrentMonth")
                        .HasColumnType("int");

                    b.Property<int>("CurrentYear")
                        .HasColumnType("int");

                    b.HasKey("WorldID");

                    b.ToTable("WorldDate", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.WorldMeta", b =>
                {
                    b.Property<Guid>("WorldID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MetaID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorldID", "MetaID");

                    b.HasIndex("MetaID");

                    b.ToTable("WorldMetas", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.Consonant", b =>
                {
                    b.HasBaseType("EpochApp.Shared.Config.Phoneme");

                    b.Property<bool>("IsVoiced")
                        .HasColumnType("bit");

                    b.Property<string>("Manner")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Place")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.ToTable("lkConsonants", "Lookups");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.Vowel", b =>
                {
                    b.HasBaseType("EpochApp.Shared.Config.Phoneme");

                    b.Property<string>("Depth")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<bool>("IsRounded")
                        .HasColumnType("bit");

                    b.Property<string>("Verticality")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.ToTable("lkVowels", "Lookups");
                });

            modelBuilder.Entity("EpochApp.Shared.Blog", b =>
                {
                    b.HasOne("EpochApp.Shared.Config.BlogType", "BlogType")
                        .WithMany()
                        .HasForeignKey("BlogTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Blogs_BlogTypes");

                    b.Navigation("BlogType");
                });

            modelBuilder.Entity("EpochApp.Shared.BlogOwner", b =>
                {
                    b.HasOne("EpochApp.Shared.Blog", "Blog")
                        .WithMany("BlogOwners")
                        .HasForeignKey("BlogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BlogOwners_Blogs");

                    b.HasOne("EpochApp.Shared.Users.User", "Owner")
                        .WithMany("OwnedBlogs")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BlogOwners_Users");

                    b.Navigation("Blog");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("EpochApp.Shared.BlogPost", b =>
                {
                    b.HasOne("EpochApp.Shared.Blog", "Blog")
                        .WithMany("BlogPosts")
                        .HasForeignKey("BlogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BlogPosts_Blogs");

                    b.HasOne("EpochApp.Shared.Post", "Post")
                        .WithMany("BlogPosts")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BlogPosts_Posts");

                    b.Navigation("Blog");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.MetaTemplate", b =>
                {
                    b.HasOne("EpochApp.Shared.Config.MetaCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MetaTemplates_MetaCategories");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EpochApp.Shared.Post", b =>
                {
                    b.HasOne("EpochApp.Shared.Users.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID")
                        .HasConstraintName("FK_Posts_Users");

                    b.HasOne("EpochApp.Shared.Config.PostType", "PostType")
                        .WithMany()
                        .HasForeignKey("PostTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("PostType");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.Profile", b =>
                {
                    b.HasOne("EpochApp.Shared.Users.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("EpochApp.Shared.Users.Profile", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Profile_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.UserRole", b =>
                {
                    b.HasOne("EpochApp.Shared.Users.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_Roles");

                    b.HasOne("EpochApp.Shared.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_Users");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.UserSocial", b =>
                {
                    b.HasOne("EpochApp.Shared.Config.SocialMedia", "Social")
                        .WithMany()
                        .HasForeignKey("SocialID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserSocials_SocialMediae");

                    b.HasOne("EpochApp.Shared.Users.Profile", "Profile")
                        .WithMany("Socials")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserSocials_Users");

                    b.Navigation("Profile");

                    b.Navigation("Social");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.World", b =>
                {
                    b.HasOne("EpochApp.Shared.Users.User", "Owner")
                        .WithMany("OwnedWorlds")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Worlds_Users");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.WorldDate", b =>
                {
                    b.HasOne("EpochApp.Shared.Worlds.World", "World")
                        .WithOne("CurrentWorldDate")
                        .HasForeignKey("EpochApp.Shared.Worlds.WorldDate", "WorldID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_WorldDates_Worlds");

                    b.Navigation("World");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.WorldMeta", b =>
                {
                    b.HasOne("EpochApp.Shared.Config.MetaTemplate", "Template")
                        .WithMany()
                        .HasForeignKey("MetaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_WorldMetas_MetaTemplates");

                    b.HasOne("EpochApp.Shared.Worlds.World", "World")
                        .WithMany("MetaData")
                        .HasForeignKey("WorldID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");

                    b.Navigation("World");
                });

            modelBuilder.Entity("EpochApp.Shared.Config.Consonant", b =>
                {
                    b.HasOne("EpochApp.Shared.Config.Phoneme", null)
                        .WithOne()
                        .HasForeignKey("EpochApp.Shared.Config.Consonant", "PhonemeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EpochApp.Shared.Config.Vowel", b =>
                {
                    b.HasOne("EpochApp.Shared.Config.Phoneme", null)
                        .WithOne()
                        .HasForeignKey("EpochApp.Shared.Config.Vowel", "PhonemeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EpochApp.Shared.Blog", b =>
                {
                    b.Navigation("BlogOwners");

                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("EpochApp.Shared.Post", b =>
                {
                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.Profile", b =>
                {
                    b.Navigation("Socials");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EpochApp.Shared.Users.User", b =>
                {
                    b.Navigation("OwnedBlogs");

                    b.Navigation("OwnedWorlds");

                    b.Navigation("Profile");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("EpochApp.Shared.Worlds.World", b =>
                {
                    b.Navigation("CurrentWorldDate");

                    b.Navigation("MetaData");
                });
#pragma warning restore 612, 618
        }
    }
}
