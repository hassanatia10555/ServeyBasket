using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editPollTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Polls",
                newName: "Summary");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndsAt",
                table: "Polls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Polls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartsAt",
                table: "Polls",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndsAt",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "StartsAt",
                table: "Polls");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Polls",
                newName: "Description");
        }
    }
}
