using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldDurationTimeSpanToDirationInMInutesInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Assistances");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Assistances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Assistances");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Assistances",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
