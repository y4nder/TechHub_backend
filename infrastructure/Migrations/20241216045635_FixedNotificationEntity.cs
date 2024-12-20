using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedNotificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "UserNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "UserNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ArticleId",
                table: "UserNotifications",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_CommentId",
                table: "UserNotifications",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Article_ArticleId",
                table: "UserNotifications",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "articleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Comment_CommentId",
                table: "UserNotifications",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "commentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Article_ArticleId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Comment_CommentId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_ArticleId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_CommentId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "UserNotifications");

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
    }
}
