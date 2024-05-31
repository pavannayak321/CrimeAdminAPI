using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrimeAdminAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedtheCrimeVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrimeMedia",
                table: "crimes",
                newName: "CrimeVideo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrimeVideo",
                table: "crimes",
                newName: "CrimeMedia");
        }
    }
}
