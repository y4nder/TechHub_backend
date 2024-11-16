using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedarticlebodynullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ArticleBo__artic__6EF57B66",
                table: "ArticleBody");

            migrationBuilder.AlterColumn<int>(
                name: "articleId",
                table: "ArticleBody",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "articleContent",
                table: "ArticleBody",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__ArticleBo__artic__6EF57B66",
                table: "ArticleBody",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ArticleBo__artic__6EF57B66",
                table: "ArticleBody");

            migrationBuilder.AlterColumn<int>(
                name: "articleId",
                table: "ArticleBody",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "articleContent",
                table: "ArticleBody",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK__ArticleBo__artic__6EF57B66",
                table: "ArticleBody",
                column: "articleId",
                principalTable: "Article",
                principalColumn: "articleId");
        }
    }
}
