using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeShit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ArticleId",
                table: "Notifications",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CommentId",
                table: "Notifications",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Article_ArticleId",
                table: "Notifications",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "articleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Comment_CommentId",
                table: "Notifications",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "commentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Article_ArticleId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Comment_CommentId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ArticleId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CommentId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Notifications");
        }
    }
}
