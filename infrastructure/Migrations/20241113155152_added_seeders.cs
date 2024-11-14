using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_seeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tagName",
                table: "Tag",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "tagCount",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "clubCategoryName",
                table: "ClubCategory",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ClubCategory",
                columns: new[] { "clubCategoryId", "clubCategoryName" },
                values: new object[,]
                {
                    { 1, "Programming Languages" },
                    { 2, "DevOps" },
                    { 3, "Artificial Intelligence" },
                    { 4, "Web Development" },
                    { 5, "Mobile Development" },
                    { 6, "Game Development" },
                    { 7, "Data Science" },
                    { 8, "Cybersecurity" },
                    { 9, "Cloud Computing" },
                    { 10, "Software Engineering" }
                });

            migrationBuilder.InsertData(
                table: "ClubUserRole",
                columns: new[] { "roleId", "roleName" },
                values: new object[,]
                {
                    { 1, "Regular User" },
                    { 2, "Club Creator" },
                    { 3, "Moderator" },
                    { 4, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "tagId", "tagName" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "Python" },
                    { 3, "JavaScript" },
                    { 4, "Java" },
                    { 5, "Ruby" },
                    { 6, "SQL" },
                    { 7, "C++" },
                    { 8, "TypeScript" },
                    { 9, "Go" },
                    { 10, "Kotlin" },
                    { 11, "Algorithms" },
                    { 12, "Data Structures" },
                    { 13, "Operating Systems" },
                    { 14, "Machine Learning" },
                    { 15, "Artificial Intelligence" },
                    { 16, "Networking" },
                    { 17, "Cybersecurity" },
                    { 18, "Databases" },
                    { 19, "Blockchain" },
                    { 20, "Cloud Computing" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ClubCategory",
                keyColumn: "clubCategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ClubUserRole",
                keyColumn: "roleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClubUserRole",
                keyColumn: "roleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClubUserRole",
                keyColumn: "roleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClubUserRole",
                keyColumn: "roleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "tagId",
                keyValue: 20);

            migrationBuilder.AlterColumn<string>(
                name: "tagName",
                table: "Tag",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "tagCount",
                table: "Tag",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "clubCategoryName",
                table: "ClubCategory",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);
        }
    }
}
