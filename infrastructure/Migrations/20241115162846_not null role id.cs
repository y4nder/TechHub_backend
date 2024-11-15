using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notnullroleid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter the RoleId column to be non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "roleId",
                table: "ClubUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Re-add the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubUserRole",
                table: "ClubUser",
                columns: new[] { "clubId", "userId", "roleId" });
        }
    
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubUserRole",
                table: "ClubUser");

            // Revert RoleId column to nullable
            migrationBuilder.AlterColumn<int>(
                name: "roleId",
                table: "ClubUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
