using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrimeAdminAPI.Migrations
{
    /// <inheritdoc />
    public partial class mappedimagevideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CrimeVideo",
                table: "crimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "crimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrimeVideo",
                table: "crimes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "crimes");
        }
    }
}
