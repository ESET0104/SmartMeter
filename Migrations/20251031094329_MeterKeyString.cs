using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class MeterKeyString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE SEQUENCE IF NOT EXISTS ""MeterSeq"" START 1 INCREMENT 1;");

            migrationBuilder.AlterColumn<string>(
                name: "MeterSerialNo",
                table: "Meters",
                type: "text",
                nullable: false,
                defaultValueSql: "'SM' || LPAD(nextval('\"MeterSeq\"')::text, 5, '0')",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "MeterSeq");

            migrationBuilder.AlterColumn<string>(
                name: "MeterSerialNo",
                table: "Meters",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "'SM' || LPAD(nextval('\"MeterSeq\"')::text, 5, '0')");
        }
    }
}
