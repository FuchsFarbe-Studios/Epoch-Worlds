﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EpochApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class BlogOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Blogs");

            migrationBuilder.EnsureSchema(
                name: "Lookups");

            migrationBuilder.EnsureSchema(
                name: "Config");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "BlogTypes",
                schema: "Blogs",
                columns: table => new
                {
                    BlogTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Description = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTypes", x => x.BlogTypeID);
                });

            migrationBuilder.CreateTable(
                name: "lkArticleCategories",
                schema: "Lookups",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkArticleCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "lkMetaCategories",
                schema: "Lookups",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkMetaCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "lkPhonemes",
                schema: "Lookups",
                columns: table => new
                {
                    PhonemeID = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    AudioFile = table.Column<string>(type: "varchar(155)", maxLength: 155, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkPhonemes", x => x.PhonemeID);
                });

            migrationBuilder.CreateTable(
                name: "lkSocialMediae",
                schema: "Lookups",
                columns: table => new
                {
                    SocialID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    URLAffix = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkSocialMediae", x => x.SocialID);
                });

            migrationBuilder.CreateTable(
                name: "PostTypes",
                schema: "Blogs",
                columns: table => new
                {
                    PostTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Description = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTypes", x => x.PostTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Users",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Description = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "longchar", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    PasswordHash = table.Column<string>(type: "longchar", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "longbinary", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateRemoved = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "Blogs",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    BlogTypeID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "longchar", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "longchar", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogID);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogTypes",
                        column: x => x.BlogTypeID,
                        principalSchema: "Blogs",
                        principalTable: "BlogTypes",
                        principalColumn: "BlogTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetaTemplates",
                schema: "Config",
                columns: table => new
                {
                    TemplateID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Placeholder = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    HelpText = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaTemplates", x => x.TemplateID);
                    table.ForeignKey(
                        name: "FK_MetaTemplates_MetaCategories",
                        column: x => x.CategoryID,
                        principalSchema: "Lookups",
                        principalTable: "lkMetaCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lkConsonants",
                schema: "Lookups",
                columns: table => new
                {
                    PhonemeID = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    Manner = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true),
                    Place = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true),
                    IsVoiced = table.Column<bool>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkConsonants", x => x.PhonemeID);
                    table.ForeignKey(
                        name: "FK_lkConsonants_lkPhonemes_PhonemeID",
                        column: x => x.PhonemeID,
                        principalSchema: "Lookups",
                        principalTable: "lkPhonemes",
                        principalColumn: "PhonemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lkVowels",
                schema: "Lookups",
                columns: table => new
                {
                    PhonemeID = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    Depth = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true),
                    Verticality = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true),
                    IsRounded = table.Column<bool>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkVowels", x => x.PhonemeID);
                    table.ForeignKey(
                        name: "FK_lkVowels_lkPhonemes_PhonemeID",
                        column: x => x.PhonemeID,
                        principalSchema: "Lookups",
                        principalTable: "lkPhonemes",
                        principalColumn: "PhonemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Blogs",
                columns: table => new
                {
                    PostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostTypeID = table.Column<int>(type: "integer", nullable: false),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "longchar", nullable: true),
                    Content = table.Column<string>(type: "longchar", nullable: true),
                    Href = table.Column<string>(type: "longchar", nullable: true),
                    OutsideLink = table.Column<string>(type: "longchar", nullable: true),
                    PostedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_PostTypes_PostTypeID",
                        column: x => x.PostTypeID,
                        principalSchema: "Blogs",
                        principalTable: "PostTypes",
                        principalColumn: "PostTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "longchar", nullable: true),
                    LastName = table.Column<string>(type: "longchar", nullable: true),
                    Bio = table.Column<string>(type: "longchar", nullable: true),
                    Signature = table.Column<string>(type: "longchar", nullable: true),
                    AvatarImg = table.Column<string>(type: "longchar", nullable: true),
                    CoverImg = table.Column<string>(type: "longchar", nullable: true),
                    WebAddress = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Profile_User",
                        column: x => x.UserID,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateRemoved = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Users",
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogOwners",
                schema: "Blogs",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "integer", nullable: false),
                    OwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    RemovedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogOwners", x => new { x.BlogID, x.OwnerID });
                    table.ForeignKey(
                        name: "FK_BlogOwners_Blogs",
                        column: x => x.BlogID,
                        principalSchema: "Blogs",
                        principalTable: "Blogs",
                        principalColumn: "BlogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogOwners_Users",
                        column: x => x.OwnerID,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                schema: "Blogs",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "integer", nullable: false),
                    PostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => new { x.BlogID, x.PostID });
                    table.ForeignKey(
                        name: "FK_BlogPosts_Blogs",
                        column: x => x.BlogID,
                        principalSchema: "Blogs",
                        principalTable: "Blogs",
                        principalColumn: "BlogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Posts",
                        column: x => x.PostID,
                        principalSchema: "Blogs",
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSocials",
                schema: "Users",
                columns: table => new
                {
                    SocialID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialHandle = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocials", x => new { x.SocialID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserSocials_Profiles_UserID",
                        column: x => x.UserID,
                        principalSchema: "Users",
                        principalTable: "Profiles",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSocials_lkSocialMediae_SocialID",
                        column: x => x.SocialID,
                        principalSchema: "Lookups",
                        principalTable: "lkSocialMediae",
                        principalColumn: "SocialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Blogs",
                table: "BlogTypes",
                columns: new[] { "BlogTypeID", "Description" },
                values: new object[,]
                {
                    { 1, "NEWS" },
                    { 2, "UPDATES" },
                    { 3, "EVENTS" },
                    { 4, "FAQ" },
                    { 5, "DOCUMENTATION" }
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "Roles",
                columns: new[] { "RoleID", "Description" },
                values: new object[,]
                {
                    { 1, "DEFAULT" },
                    { 2, "ACTIVATED" },
                    { 3, "MODERATOR" },
                    { 4, "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogOwners_OwnerID",
                schema: "Blogs",
                table: "BlogOwners",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_PostID",
                schema: "Blogs",
                table: "BlogPosts",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogTypeID",
                schema: "Blogs",
                table: "Blogs",
                column: "BlogTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MetaTemplates_CategoryID",
                schema: "Config",
                table: "MetaTemplates",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                schema: "Blogs",
                table: "Posts",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTypeID",
                schema: "Blogs",
                table: "Posts",
                column: "PostTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                schema: "Users",
                table: "UserRoles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocials_UserID",
                schema: "Users",
                table: "UserSocials",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogOwners",
                schema: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogPosts",
                schema: "Blogs");

            migrationBuilder.DropTable(
                name: "lkArticleCategories",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "lkConsonants",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "lkVowels",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "MetaTemplates",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "UserSocials",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "Blogs");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Blogs");

            migrationBuilder.DropTable(
                name: "lkPhonemes",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "lkMetaCategories",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "lkSocialMediae",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "BlogTypes",
                schema: "Blogs");

            migrationBuilder.DropTable(
                name: "PostTypes",
                schema: "Blogs");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Users");
        }
    }
}