using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class IDAddedInAddressdeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Addresses",
                newName: "Id");
        }
    }
}
