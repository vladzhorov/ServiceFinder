using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPROFILEnAVIGATION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileEntityId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "UserProfileEntityId",
                table: "Reviews",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserProfileEntityId",
                table: "Reviews",
                newName: "IX_Reviews_UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileId",
                table: "Reviews",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Reviews",
                newName: "UserProfileEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserProfileId",
                table: "Reviews",
                newName: "IX_Reviews_UserProfileEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileEntityId",
                table: "Reviews",
                column: "UserProfileEntityId",
                principalTable: "UserProfile",
                principalColumn: "Id");
        }
    }
}
