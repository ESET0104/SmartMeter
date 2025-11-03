using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrgUnitId",
                table: "Consumers",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_TariffId",
                table: "Consumers",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_OrgUnits_OrgUnitId",
                table: "Consumers",
                column: "OrgUnitId",
                principalTable: "OrgUnits",
                principalColumn: "OrgUnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Tariffs_TariffId",
                table: "Consumers",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "TariffId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_OrgUnits_OrgUnitId",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Tariffs_TariffId",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_OrgUnitId",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_TariffId",
                table: "Consumers");
        }
    }
}
