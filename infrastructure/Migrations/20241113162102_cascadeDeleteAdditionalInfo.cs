using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDeleteAdditionalInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UserAddit__userI__5EBF139D",
                table: "UserAdditionalInfo");

            migrationBuilder.AddForeignKey(
                name: "FK__UserAddit__userI__5EBF139D",
                table: "UserAdditionalInfo",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UserAddit__userI__5EBF139D",
                table: "UserAdditionalInfo");

            migrationBuilder.AddForeignKey(
                name: "FK__UserAddit__userI__5EBF139D",
                table: "UserAdditionalInfo",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");
        }
    }
}
