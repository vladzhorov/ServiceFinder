using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Auth0ID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auth0Id",
                table: "UserProfile",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth0Id",
                table: "UserProfile");
        }
    }
}
