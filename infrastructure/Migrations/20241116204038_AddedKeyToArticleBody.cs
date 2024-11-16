using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedKeyToArticleBody : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ArticleBody_articleId",
                table: "ArticleBody");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleBody",
                table: "ArticleBody",
                column: "articleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleBody",
                table: "ArticleBody");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleBody_articleId",
                table: "ArticleBody",
                column: "articleId");
        }
    }
}
