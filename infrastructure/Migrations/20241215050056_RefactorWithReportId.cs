using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorWithReportId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportedArticles",
                table: "ReportedArticles");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "ReportedArticles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportedArticles",
                table: "ReportedArticles",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedArticles_ReporterId",
                table: "ReportedArticles",
                column: "ReporterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportedArticles",
                table: "ReportedArticles");

            migrationBuilder.DropIndex(
                name: "IX_ReportedArticles_ReporterId",
                table: "ReportedArticles");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "ReportedArticles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportedArticles",
                table: "ReportedArticles",
                columns: new[] { "ReporterId", "ArticleId" });
        }
    }
}
