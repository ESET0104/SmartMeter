using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    HouseNo = table.Column<string>(type: "text", nullable: true),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    PinCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arrears",
                columns: table => new
                {
                    ArrearId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    ArrearType = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidStatus = table.Column<string>(type: "text", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrears", x => x.ArrearId);
                    table.ForeignKey(
                        name: "FK_Arrears_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    BillId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    MeterId = table.Column<string>(type: "text", nullable: false),
                    BillingPeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    BillingPeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalUnitsConsumed = table.Column<decimal>(type: "numeric", nullable: false),
                    BaseAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    GeneratedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaidDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    DisconnectionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Billings_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meters",
                columns: table => new
                {
                    MeterSerialNo = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    ICCID = table.Column<string>(type: "text", nullable: false),
                    IMSI = table.Column<string>(type: "text", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: false),
                    Firmware = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    InstallTsUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.MeterSerialNo);
                    table.ForeignKey(
                        name: "FK_Meters_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId");
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    TariffId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    BaseRate = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.TariffId);
                });

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                columns: table => new
                {
                    MeterReadingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MeterId = table.Column<string>(type: "text", nullable: false),
                    MeterReadingDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EnergyConsumed = table.Column<decimal>(type: "numeric", nullable: false),
                    Voltage = table.Column<decimal>(type: "numeric", nullable: false),
                    Current = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => x.MeterReadingId);
                    table.ForeignKey(
                        name: "FK_MeterReadings_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "MeterSerialNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TariffSlabs",
                columns: table => new
                {
                    TariffSlabId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TariffId = table.Column<int>(type: "integer", nullable: false),
                    FromKwh = table.Column<decimal>(type: "numeric", nullable: false),
                    ToKwh = table.Column<decimal>(type: "numeric", nullable: false),
                    RatePerKwh = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffSlabs", x => x.TariffSlabId);
                    table.ForeignKey(
                        name: "FK_TariffSlabs_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "TariffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodRules",
                columns: table => new
                {
                    TodRuleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TariffId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    RatePerKwh = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodRules", x => x.TodRuleId);
                    table.ForeignKey(
                        name: "FK_TodRules_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "TariffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TariffDetails",
                columns: table => new
                {
                    TariffDetailsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TariffSlabId = table.Column<int>(type: "integer", nullable: true),
                    TodRuleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffDetails", x => x.TariffDetailsId);
                    table.ForeignKey(
                        name: "FK_TariffDetails_TariffSlabs_TariffSlabId",
                        column: x => x.TariffSlabId,
                        principalTable: "TariffSlabs",
                        principalColumn: "TariffSlabId");
                    table.ForeignKey(
                        name: "FK_TariffDetails_TodRules_TodRuleId",
                        column: x => x.TodRuleId,
                        principalTable: "TodRules",
                        principalColumn: "TodRuleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ConsumerId",
                table: "Addresses",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrears_ConsumerId",
                table: "Arrears",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_ConsumerId",
                table: "Billings",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_MeterId",
                table: "MeterReadings",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Meters_ConsumerId",
                table: "Meters",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffDetails_TariffSlabId",
                table: "TariffDetails",
                column: "TariffSlabId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffDetails_TodRuleId",
                table: "TariffDetails",
                column: "TodRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSlabs_TariffId",
                table: "TariffSlabs",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_TodRules_TariffId",
                table: "TodRules",
                column: "TariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Arrears");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "MeterReadings");

            migrationBuilder.DropTable(
                name: "TariffDetails");

            migrationBuilder.DropTable(
                name: "Meters");

            migrationBuilder.DropTable(
                name: "TariffSlabs");

            migrationBuilder.DropTable(
                name: "TodRules");

            migrationBuilder.DropTable(
                name: "Tariffs");
        }
    }
}
