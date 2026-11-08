using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUniqeIndexOnQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_Content",
                schema: "dbo",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_PollId",
                schema: "dbo",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PollId_Content",
                schema: "dbo",
                table: "Questions",
                columns: new[] { "PollId", "Content" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_PollId_Content",
                schema: "dbo",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Content",
                schema: "dbo",
                table: "Questions",
                column: "Content",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PollId",
                schema: "dbo",
                table: "Questions",
                column: "PollId");
        }
    }
}
