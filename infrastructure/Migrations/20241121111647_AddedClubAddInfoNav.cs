using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedClubAddInfoNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClubAddit__clubI__6754599E",
                table: "ClubAdditionalInfo");

            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "Club",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubAdditionalInfo_Club_clubId",
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
                name: "FK_ClubAdditionalInfo_Club_clubId",
                table: "ClubAdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Club");

            migrationBuilder.AddForeignKey(
                name: "FK__ClubAddit__clubI__6754599E",
                table: "ClubAdditionalInfo",
                column: "clubId",
                principalTable: "Club",
                principalColumn: "clubId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
