using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MeterSeq");

            migrationBuilder.CreateTable(
                name: "CustomerCareMessages",
                columns: table => new
                {
                    MessageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCareMessages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrgUnits",
                columns: table => new
                {
                    OrgUnitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnits", x => x.OrgUnitId);
                    table.CheckConstraint("CK_Type", "\"Type\" IN ('Zone','Substation','Feeder','DTR')");
                    table.ForeignKey(
                        name: "FK_OrgUnits_OrgUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "OrgUnits",
                        principalColumn: "OrgUnitId");
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
                    BaseRate = table.Column<double>(type: "double precision", nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.TariffId);
                    table.CheckConstraint("CK_Tariff_BaseRate", "\"BaseRate\" > 0");
                    table.CheckConstraint("CK_Tariff_Dates", "\"EffectiveTo\" IS NULL OR \"EffectiveFrom\" < \"EffectiveTo\"");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    LastLoginUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    OrgUnitId = table.Column<int>(type: "integer", nullable: false),
                    TariffId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.ConsumerId);
                    table.CheckConstraint("CK_Consumer_timestamp", "\"UpdatedAt\" IS NULL OR \"UpdatedAt\" > \"CreatedAt\"");
                    table.ForeignKey(
                        name: "FK_Consumers_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "OrgUnitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consumers_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "TariffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TariffSlabs",
                columns: table => new
                {
                    TariffSlabId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TariffId = table.Column<int>(type: "integer", nullable: false),
                    FromKwh = table.Column<double>(type: "numeric(18,6)", nullable: false),
                    ToKwh = table.Column<double>(type: "numeric(18,6)", nullable: false),
                    RatePerKwh = table.Column<double>(type: "numeric(18,6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffSlabs", x => x.TariffSlabId);
                    table.CheckConstraint("CK_TariffSlab_Range", "\"FromKwh\" >= 0 AND \"ToKwh\" > \"FromKwh\"");
                    table.CheckConstraint("CK_TariffSlab_Rate", "\"RatePerKwh\" > 0");
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
                    RatePerKwh = table.Column<double>(type: "double precision", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodRules", x => x.TodRuleId);
                    table.CheckConstraint("CK_TodRule_Rate", "\"RatePerKwh\" > 0");
                    table.CheckConstraint("CK_TodRule_Time", "\"EndTime\" > \"StartTime\"");
                    table.ForeignKey(
                        name: "FK_TodRules_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "TariffId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    PaidStatus = table.Column<string>(type: "text", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrears", x => x.ArrearId);
                    table.CheckConstraint("CK_Arrear_Amount", "\"Amount\" >= 0");
                    table.CheckConstraint("CK_Arrear_PaidStatus", "\"PaidStatus\" IN ('Paid','Unpaid','Partially Paid')");
                    table.CheckConstraint("CK_Arrear_Type", "\"ArrearType\" IN ('Overdue','Penalty','Interest')");
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
                    TotalUnitsConsumed = table.Column<double>(type: "numeric(18,6)", nullable: false),
                    BaseAmount = table.Column<double>(type: "numeric(18,4)", nullable: false),
                    TaxAmount = table.Column<double>(type: "numeric(18,4)", nullable: false),
                    TotalAmount = table.Column<double>(type: "numeric(18,4)", nullable: false, computedColumnSql: "\"BaseAmount\" + \"TaxAmount\"", stored: true),
                    GeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentStatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DisconnectionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.BillId);
                    table.CheckConstraint("CK_Billings_PaidStatus", "\"PaymentStatus\" IN ('Paid','Unpaid','Overdue','Cancelled')");
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
                    MeterSerialNo = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'SM' || LPAD(nextval('\"MeterSeq\"')::text, 5, '0')"),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    ICCID = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IMSI = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Manufacturer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Firmware = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InstallTsUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.MeterSerialNo);
                    table.CheckConstraint("CK_Meter_Status", "\"Status\" IN ('Active','Inactive','Decommissioned')");
                    table.ForeignKey(
                        name: "FK_Meters_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId");
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

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MeterId = table.Column<string>(type: "text", nullable: false),
                    ReadingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Voltage = table.Column<double>(type: "double precision", nullable: false),
                    Current = table.Column<double>(type: "double precision", nullable: false),
                    PowerFactor = table.Column<double>(type: "double precision", nullable: false),
                    EnergyConsumed = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeterReadings_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "MeterSerialNo",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Consumers_Name",
                table: "Consumers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrgUnitId",
                table: "Consumers",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_TariffId",
                table: "Consumers",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_MeterId",
                table: "MeterReadings",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Meters_ConsumerId",
                table: "Meters",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_ParentId",
                table: "OrgUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_Type",
                table: "OrgUnits",
                column: "Type");

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
                name: "IX_TodRules_Name",
                table: "TodRules",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TodRules_TariffId",
                table: "TodRules",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
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
                name: "CustomerCareMessages");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "MeterReadings");

            migrationBuilder.DropTable(
                name: "TariffDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Meters");

            migrationBuilder.DropTable(
                name: "TariffSlabs");

            migrationBuilder.DropTable(
                name: "TodRules");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "OrgUnits");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropSequence(
                name: "MeterSeq");
        }
    }
}
