using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEvaluationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportedArticle_Article_ArticleId",
                table: "ReportedArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportedArticle_User_ReporterId",
                table: "ReportedArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportedArticle",
                table: "ReportedArticle");

            migrationBuilder.RenameTable(
                name: "ReportedArticle",
                newName: "ReportedArticles");

            migrationBuilder.RenameIndex(
                name: "IX_ReportedArticle_ReporterId",
                table: "ReportedArticles",
                newName: "IX_ReportedArticles_ReporterId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportedArticle_ArticleId",
                table: "ReportedArticles",
                newName: "IX_ReportedArticles_ArticleId");

            migrationBuilder.AddColumn<bool>(
                name: "Evaluated",
                table: "ReportedArticles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportedArticles",
                table: "ReportedArticles",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedArticles_Article_ArticleId",
                table: "ReportedArticles",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedArticles_User_ReporterId",
                table: "ReportedArticles",
                column: "ReporterId",
                principalTable: "User",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportedArticles_Article_ArticleId",
                table: "ReportedArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportedArticles_User_ReporterId",
                table: "ReportedArticles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportedArticles",
                table: "ReportedArticles");

            migrationBuilder.DropColumn(
                name: "Evaluated",
                table: "ReportedArticles");

            migrationBuilder.RenameTable(
                name: "ReportedArticles",
                newName: "ReportedArticle");

            migrationBuilder.RenameIndex(
                name: "IX_ReportedArticles_ReporterId",
                table: "ReportedArticle",
                newName: "IX_ReportedArticle_ReporterId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportedArticles_ArticleId",
                table: "ReportedArticle",
                newName: "IX_ReportedArticle_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportedArticle",
                table: "ReportedArticle",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedArticle_Article_ArticleId",
                table: "ReportedArticle",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedArticle_User_ReporterId",
                table: "ReportedArticle",
                column: "ReporterId",
                principalTable: "User",
                principalColumn: "userId");
        }
    }
}
