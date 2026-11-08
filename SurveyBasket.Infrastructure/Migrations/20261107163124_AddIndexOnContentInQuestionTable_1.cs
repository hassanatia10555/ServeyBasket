using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnContentInQuestionTable_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_Content",
                schema: "dbo",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Content",
                schema: "dbo",
                table: "Questions",
                column: "Content",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_Content",
                schema: "dbo",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Content",
                schema: "dbo",
                table: "Questions",
                column: "Content");
        }
    }
}
