using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrimeAdminAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedtheCrimeImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ImageUrl",
                table: "crimes");
        }
    }
}
