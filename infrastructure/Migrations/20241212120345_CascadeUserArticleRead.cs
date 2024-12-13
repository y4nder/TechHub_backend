using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadeUserArticleRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__User_Arti__artic__70DDC3D8",
                table: "User_Article_Read");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Arti__artic__70DDC3D8",
                table: "User_Article_Read",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__User_Arti__artic__70DDC3D8",
                table: "User_Article_Read");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Arti__artic__70DDC3D8",
                table: "User_Article_Read",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId");
        }
    }
}
