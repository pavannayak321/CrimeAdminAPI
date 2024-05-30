using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrimeAdminAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedMediaType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CrimeMedia",
                table: "crimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CrimeMedia",
                table: "crimes");
        }
    }
}
