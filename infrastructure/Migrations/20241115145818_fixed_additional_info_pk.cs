using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixed_additional_info_pk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClubAddit__clubI__6754599E",
                table: "ClubAdditionalInfo");

            migrationBuilder.DropIndex(
                name: "IX_ClubAdditionalInfo_clubId",
                table: "ClubAdditionalInfo");

            migrationBuilder.AlterColumn<int>(
                name: "clubId",
                table: "ClubAdditionalInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__ClubAdditionalInfo",
                table: "ClubAdditionalInfo",
                column: "clubId");

            migrationBuilder.AddForeignKey(
                name: "FK__ClubAddit__clubI__6754599E",
                table: "ClubAdditionalInfo",
                column: "clubId",
                principalTable: "Club",
                principalColumn: "clubId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClubAddit__clubI__6754599E",
                table: "ClubAdditionalInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ClubAdditionalInfo",
                table: "ClubAdditionalInfo");

            migrationBuilder.AlterColumn<int>(
                name: "clubId",
                table: "ClubAdditionalInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ClubAdditionalInfo_clubId",
                table: "ClubAdditionalInfo",
                column: "clubId");

            migrationBuilder.AddForeignKey(
                name: "FK__ClubAddit__clubI__6754599E",
                table: "ClubAdditionalInfo",
                column: "clubId",
                principalTable: "Club",
                principalColumn: "clubId");
        }
    }
}
