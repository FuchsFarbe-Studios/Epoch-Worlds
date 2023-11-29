using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EpochApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Users");

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
                    PasswordSalt = table.Column<byte[]>(type: "longbinary", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
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
                name: "IX_UserRoles_UserID",
                schema: "Users",
                table: "UserRoles",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Users");
        }
    }
}
