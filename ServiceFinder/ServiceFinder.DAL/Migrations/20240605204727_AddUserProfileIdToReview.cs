using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileIdToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileEntityId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserProfileEntityId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserProfileEntityId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserProfileId",
                table: "Reviews",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileId",
                table: "Reviews",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserProfileId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileEntityId",
                table: "Reviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserProfileEntityId",
                table: "Reviews",
                column: "UserProfileEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UserProfile_UserProfileEntityId",
                table: "Reviews",
                column: "UserProfileEntityId",
                principalTable: "UserProfile",
                principalColumn: "Id");
        }
    }
}
