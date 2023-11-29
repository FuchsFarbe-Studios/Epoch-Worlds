using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpochApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class Gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "Users",
                table: "Users",
                type: "longchar",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Users",
                table: "Users");
        }
    }
}
