using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovedUserProfilePicToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userProfilePicUrl",
                table: "UserAdditionalInfo");

            migrationBuilder.AddColumn<string>(
                name: "UserProfilePicUrl",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfilePicUrl",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "userProfilePicUrl",
                table: "UserAdditionalInfo",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
