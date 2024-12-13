using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadeTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ArticleTa__artic__76969D2E",
                table: "ArticleTag");

            migrationBuilder.AddForeignKey(
                name: "FK__ArticleTa__artic__76969D2E",
                table: "ArticleTag",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ArticleTa__artic__76969D2E",
                table: "ArticleTag");

            migrationBuilder.AddForeignKey(
                name: "FK__ArticleTa__artic__76969D2E",
                table: "ArticleTag",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId");
        }
    }
}
