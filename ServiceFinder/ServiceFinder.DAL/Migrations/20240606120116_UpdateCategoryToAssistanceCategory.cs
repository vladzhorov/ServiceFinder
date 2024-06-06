using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryToAssistanceCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_AssistanceCategories_CategoryId",
                table: "Assistances");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_CategoryId",
                table: "Assistances");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Assistances");

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_AssistanceCategoryId",
                table: "Assistances",
                column: "AssistanceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_AssistanceCategories_AssistanceCategoryId",
                table: "Assistances",
                column: "AssistanceCategoryId",
                principalTable: "AssistanceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_AssistanceCategories_AssistanceCategoryId",
                table: "Assistances");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_AssistanceCategoryId",
                table: "Assistances");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Assistances",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_CategoryId",
                table: "Assistances",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_AssistanceCategories_CategoryId",
                table: "Assistances",
                column: "CategoryId",
                principalTable: "AssistanceCategories",
                principalColumn: "Id");
        }
    }
}
