using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTagDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagDescription",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 1,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 2,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 3,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 4,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 5,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 6,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 7,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 8,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 9,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 10,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 11,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 12,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 13,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 14,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 15,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 16,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 17,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 18,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 19,
                column: "TagDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 20,
                column: "TagDescription",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagDescription",
                table: "Tag");
        }
    }
}
