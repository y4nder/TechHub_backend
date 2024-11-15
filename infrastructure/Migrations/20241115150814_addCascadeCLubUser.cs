using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeCLubUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClubUser__clubId__6477ECF3",
                table: "ClubUser");

            migrationBuilder.DropForeignKey(
                name: "FK__ClubUser__userId__656C112C",
                table: "ClubUser");

            migrationBuilder.AddForeignKey(
                name: "FK__ClubUser__clubId__6477ECF3",
                table: "ClubUser",
                column: "clubId",
                principalTable: "Club",
                principalColumn: "clubId");

            migrationBuilder.AddForeignKey(
                name: "FK__ClubUser__userId__656C112C",
                table: "ClubUser",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ClubUser__clubId__6477ECF3",
                table: "ClubUser");

            migrationBuilder.DropForeignKey(
                name: "FK__ClubUser__userId__656C112C",
                table: "ClubUser");

            migrationBuilder.AddForeignKey(
                name: "FK__ClubUser__clubId__6477ECF3",
                table: "ClubUser",
                column: "clubId",
                principalTable: "Club",
                principalColumn: "clubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__ClubUser__userId__656C112C",
                table: "ClubUser",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
