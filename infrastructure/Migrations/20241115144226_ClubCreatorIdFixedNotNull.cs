using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClubCreatorIdFixedNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Club__clubCatego__6383C8BA",
                table: "Club");

            migrationBuilder.DropForeignKey(
                name: "FK__Club__clubCreato__628FA481",
                table: "Club");

            migrationBuilder.AlterColumn<int>(
                name: "clubCreatorId",
                table: "Club",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Club__clubCatego__6383C8BA",
                table: "Club",
                column: "clubCategoryId",
                principalTable: "ClubCategory",
                principalColumn: "clubCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Club__clubCreato__628FA481",
                table: "Club",
                column: "clubCreatorId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Club__clubCatego__6383C8BA",
                table: "Club");

            migrationBuilder.DropForeignKey(
                name: "FK__Club__clubCreato__628FA481",
                table: "Club");

            migrationBuilder.AlterColumn<int>(
                name: "clubCreatorId",
                table: "Club",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__Club__clubCatego__6383C8BA",
                table: "Club",
                column: "clubCategoryId",
                principalTable: "ClubCategory",
                principalColumn: "clubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK__Club__clubCreato__628FA481",
                table: "Club",
                column: "clubCreatorId",
                principalTable: "User",
                principalColumn: "userId");
        }
    }
}
