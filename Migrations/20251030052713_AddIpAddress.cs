using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddIpAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuccessfull",
                table: "LoginLogs",
                newName: "IsSuccess");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "LoginLogs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "LoginLogs");

            migrationBuilder.RenameColumn(
                name: "IsSuccess",
                table: "LoginLogs",
                newName: "IsSuccessfull");
        }
    }
}
