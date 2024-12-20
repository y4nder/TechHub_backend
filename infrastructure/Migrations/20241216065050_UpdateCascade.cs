using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UserArtic__artic__72C60C4A",
                table: "UserArticleBookmark");

            migrationBuilder.DropForeignKey(
                name: "FK__UserArtic__artic__74AE54BC",
                table: "UserArticleVote");

            migrationBuilder.DropForeignKey(
                name: "FK__UserComme__comme__6C190EBB",
                table: "UserCommentVote");

            migrationBuilder.AddForeignKey(
                name: "FK__UserArtic__artic__72C60C4A",
                table: "UserArticleBookmark",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UserArtic__artic__74AE54BC",
                table: "UserArticleVote",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UserComme__comme__6C190EBB",
                table: "UserCommentVote",
                column: "commentId",
                principalTable: "Comment",
                principalColumn: "commentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UserArtic__artic__72C60C4A",
                table: "UserArticleBookmark");

            migrationBuilder.DropForeignKey(
                name: "FK__UserArtic__artic__74AE54BC",
                table: "UserArticleVote");

            migrationBuilder.DropForeignKey(
                name: "FK__UserComme__comme__6C190EBB",
                table: "UserCommentVote");

            migrationBuilder.AddForeignKey(
                name: "FK__UserArtic__artic__72C60C4A",
                table: "UserArticleBookmark",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId");

            migrationBuilder.AddForeignKey(
                name: "FK__UserArtic__artic__74AE54BC",
                table: "UserArticleVote",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId");

            migrationBuilder.AddForeignKey(
                name: "FK__UserComme__comme__6C190EBB",
                table: "UserCommentVote",
                column: "commentId",
                principalTable: "Comment",
                principalColumn: "commentId");
        }
    }
}
