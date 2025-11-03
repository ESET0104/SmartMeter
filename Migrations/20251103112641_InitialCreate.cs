using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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

            migrationBuilder.InsertData(
                table: "Meters",
                columns: new[] { "MeterSerialNo", "Category", "ConsumerId", "Firmware", "ICCID", "IMSI", "InstallTsUtc", "IpAddress", "Manufacturer", "Status" },
                values: new object[] { "SC23S00078", "Three Phase", null, "v3.3.1", "899110234567890123F", "404112345678906", new DateTimeOffset(new DateTime(2023, 11, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "10.0.0.10", "Secure Meters", "Inactive" });

            migrationBuilder.InsertData(
                table: "OrgUnits",
                columns: new[] { "OrgUnitId", "Name", "ParentId", "Type" },
                values: new object[] { 1, "North Zone", null, "Zone" });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "TariffId", "BaseRate", "EffectiveFrom", "EffectiveTo", "Name", "TaxRate" },
                values: new object[,]
                {
                    { 1, 90.0, new DateOnly(2024, 1, 1), null, "Residential Basic", 0.10000000000000001 },
                    { 2, 150.0, new DateOnly(2024, 1, 1), null, "Commercial Standard", 0.12 },
                    { 3, 700.0, new DateOnly(2024, 1, 1), null, "Industrial High-Load", 0.14999999999999999 },
                    { 4, 60.0, new DateOnly(2024, 1, 1), null, "Agricultural Subsidized", 0.050000000000000003 },
                    { 5, 120.0, new DateOnly(2024, 6, 1), null, "EV Charging Tariff", 0.080000000000000002 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DisplayName", "Email", "IsActive", "LastLoginUtc", "PasswordHash", "Phone", "UserName" },
                values: new object[,]
                {
                    { 1L, "System Administrator", "admin@smartmeter.com", true, null, "$2a$11$nG48 / pPMTnCvehLK.ebbXeEXvw5XqJwPLkHPsH5Cuc2466gvIrP8C", "9876543210", "admin" },
                    { 2L, "Ganesh", "ganesh@smartmeter.com", true, null, "$2a$11$nlUMkcxzYytmlcZWNuDQ0enFXhKDKD9gAHtr45pHlKA.eaPJ9Vi/.", "9123456780", "ganesh" },
                    { 3L, "Support Engineer", "support1@smartmeter.com", true, null, "$2a$11$6EK8mbMOJbJLh0adk1hiQeWeij8lW3OeEeOHyB4/aRRp4Tv1nxCfS", "9998887770", "support1" },
                    { 4L, "Energy Auditor", "auditor@smartmeter.com", true, null, "$2a$11$U1yXqZUuWV3AoF8Nv78NlePvk7.65UckxVZQ3Nn3qoSdtjpkL9Nn6", "9988776655", "auditor" },
                    { 5L, "Viewer Account", "viewer@smartmeter.com", true, null, "$2a$11$4A4WI6j0RrYKDbl6ZAmhze6JNH..fZL98NexIsYTAR/Jk9JtFoZv6", "9876501234", "viewer" }
                });

            migrationBuilder.InsertData(
                table: "OrgUnits",
                columns: new[] { "OrgUnitId", "Name", "ParentId", "Type" },
                values: new object[] { 2, "Maplewood Substation", 1, "Substation" });

            migrationBuilder.InsertData(
                table: "TariffSlabs",
                columns: new[] { "TariffSlabId", "FromKwh", "IsDeleted", "RatePerKwh", "TariffId", "ToKwh" },
                values: new object[,]
                {
                    { 1, 0.0, false, 4.5, 1, 100.0 },
                    { 2, 100.000001, false, 6.0, 1, 200.0 },
                    { 3, 200.000001, false, 8.5, 1, 500.0 },
                    { 4, 0.0, false, 7.0, 2, 500.0 },
                    { 5, 500.000001, false, 9.25, 2, 1000.0 },
                    { 6, 1000.000001, false, 11.0, 2, 5000.0 },
                    { 7, 0.0, false, 9.5, 3, 10000.0 },
                    { 8, 10000.000001, false, 12.0, 3, 100000.0 },
                    { 9, 0.0, false, 2.5, 4, 300.0 },
                    { 10, 300.000001, false, 3.5, 4, 1000.0 },
                    { 11, 0.0, false, 5.75, 5, 50.0 },
                    { 12, 50.000000999999997, false, 6.5, 5, 200.0 }
                });

            migrationBuilder.InsertData(
                table: "TodRules",
                columns: new[] { "TodRuleId", "EndTime", "IsDeleted", "Name", "RatePerKwh", "StartTime", "TariffId" },
                values: new object[,]
                {
                    { 1, new TimeOnly(22, 0, 0), false, "Peak Hours", 10.5, new TimeOnly(18, 0, 0), 2 },
                    { 2, new TimeOnly(23, 59, 59), false, "Off-Peak Hours (Evening)", 5.5, new TimeOnly(22, 0, 0), 2 },
                    { 3, new TimeOnly(18, 0, 0), false, "Standard Hours", 7.5, new TimeOnly(6, 0, 0), 2 },
                    { 4, new TimeOnly(23, 0, 0), false, "Industrial Peak", 13.0, new TimeOnly(17, 0, 0), 3 },
                    { 5, new TimeOnly(23, 59, 59), false, "Industrial Off-Peak (Evening)", 7.0, new TimeOnly(23, 0, 0), 3 },
                    { 6, new TimeOnly(23, 59, 59), false, "EV Super Off-Peak (Evening)", 4.5, new TimeOnly(23, 0, 0), 5 },
                    { 7, new TimeOnly(22, 0, 0), false, "EV Peak", 8.0, new TimeOnly(19, 0, 0), 5 },
                    { 8, new TimeOnly(6, 0, 0), false, "Off-Peak Hours (Morning)", 5.5, new TimeOnly(0, 0, 0), 2 },
                    { 9, new TimeOnly(5, 0, 0), false, "Industrial Off-Peak (Morning)", 7.0, new TimeOnly(0, 0, 0), 3 },
                    { 10, new TimeOnly(5, 0, 0), false, "EV Super Off-Peak (Morning)", 4.5, new TimeOnly(0, 0, 0), 5 }
                });

            migrationBuilder.InsertData(
                table: "OrgUnits",
                columns: new[] { "OrgUnitId", "Name", "ParentId", "Type" },
                values: new object[,]
                {
                    { 3, "Feeder F-11", 2, "Feeder" },
                    { 4, "Feeder F-12", 2, "Feeder" }
                });

            migrationBuilder.InsertData(
                table: "TariffDetails",
                columns: new[] { "TariffDetailsId", "TariffSlabId", "TodRuleId" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 2, 2, null },
                    { 3, 3, null },
                    { 4, 4, null },
                    { 5, 5, null },
                    { 6, 6, null },
                    { 7, 7, null },
                    { 8, 8, null },
                    { 9, 9, null },
                    { 10, 10, null },
                    { 11, 11, null },
                    { 12, 12, null },
                    { 13, null, 1 },
                    { 14, null, 2 },
                    { 15, null, 3 },
                    { 16, null, 4 },
                    { 17, null, 5 },
                    { 18, null, 6 },
                    { 19, null, 7 },
                    { 20, null, 8 },
                    { 21, null, 9 },
                    { 22, null, 10 }
                });

            migrationBuilder.InsertData(
                table: "OrgUnits",
                columns: new[] { "OrgUnitId", "Name", "ParentId", "Type" },
                values: new object[,]
                {
                    { 5, "DTR-11045", 3, "DTR" },
                    { 6, "DTR-12001", 4, "DTR" }
                });

            migrationBuilder.InsertData(
                table: "Consumers",
                columns: new[] { "ConsumerId", "CreatedAt", "CreatedBy", "Email", "IsDeleted", "Name", "OrgUnitId", "PasswordHash", "Phone", "PhotoPath", "Status", "TariffId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "priya.s@example.com", false, "Priya Sharma", 5, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543211", null, "Active", 1, null, null },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "rohan.k@example.com", false, "Rohan Kumar", 5, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543212", null, "Active", 2, null, null },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "vikram.s@example.com", false, "Vikram Singh", 6, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543213", null, "Active", 1, null, null },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "anjali.d@example.com", false, "Anjali Devi", 6, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543214", null, "Active", 5, null, null },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "contact@guptaindustries.com", false, "Gupta Industries", 5, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543215", null, "Active", 3, null, null }
                });

            migrationBuilder.InsertData(
                table: "Billings",
                columns: new[] { "BillId", "BaseAmount", "BillingPeriodEnd", "BillingPeriodStart", "ConsumerId", "DisconnectionDate", "DueDate", "GeneratedAt", "MeterId", "PaidDate", "PaymentStatus", "TaxAmount", "TotalUnitsConsumed" },
                values: new object[,]
                {
                    { 1L, 901.79999999999995, new DateOnly(2025, 9, 1), new DateOnly(2025, 8, 1), 1L, null, new DateOnly(2025, 9, 15), new DateTime(2025, 9, 1, 5, 0, 0, 0, DateTimeKind.Utc), "GN24A00187", new DateTime(2025, 9, 10, 10, 30, 0, 0, DateTimeKind.Utc), "Paid", 90.180000000000007, 175.30000000000001 },
                    { 2L, 7296.1199999999999, new DateOnly(2025, 9, 1), new DateOnly(2025, 8, 1), 2L, null, new DateOnly(2025, 9, 15), new DateTime(2025, 9, 1, 5, 0, 0, 0, DateTimeKind.Utc), "LT24C00255", new DateTime(2025, 9, 8, 14, 0, 0, 0, DateTimeKind.Utc), "Paid", 875.52999999999997, 910.5 },
                    { 3L, 1264.2, new DateOnly(2025, 9, 1), new DateOnly(2025, 8, 1), 3L, null, new DateOnly(2025, 9, 15), new DateTime(2025, 9, 1, 5, 0, 0, 0, DateTimeKind.Utc), "GN24A00193", new DateTime(2025, 9, 12, 11, 45, 0, 0, DateTimeKind.Utc), "Paid", 126.42, 225.19999999999999 },
                    { 4L, 966.75, new DateOnly(2025, 9, 1), new DateOnly(2025, 8, 1), 4L, null, new DateOnly(2025, 9, 15), new DateTime(2025, 9, 1, 5, 0, 0, 0, DateTimeKind.Utc), "HP24E00301", new DateTime(2025, 9, 7, 16, 20, 0, 0, DateTimeKind.Utc), "Paid", 77.340000000000003, 154.5 },
                    { 5L, 121655.60000000001, new DateOnly(2025, 9, 1), new DateOnly(2025, 8, 1), 5L, null, new DateOnly(2025, 9, 15), new DateTime(2025, 9, 1, 5, 0, 0, 0, DateTimeKind.Utc), "LT24I00419", new DateTime(2025, 9, 5, 9, 15, 0, 0, DateTimeKind.Utc), "Paid", 18248.34, 12221.299999999999 }
                });

            migrationBuilder.InsertData(
                table: "Meters",
                columns: new[] { "MeterSerialNo", "Category", "ConsumerId", "Firmware", "ICCID", "IMSI", "InstallTsUtc", "IpAddress", "Manufacturer", "Status" },
                values: new object[,]
                {
                    { "GN24A00187", "Single Phase", 1L, "v2.1.3", "899110234567890123A", "404112345678901", new DateTimeOffset(new DateTime(2024, 2, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "192.168.1.101", "Genus Power", "Active" },
                    { "GN24A00193", "Single Phase", 3L, "v2.1.5", "899110234567890123C", "404112345678903", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "192.168.2.101", "Genus Power", "Active" },
                    { "HP24E00301", "Single Phase EV", 4L, "v1.8.0", "899110234567890123D", "404112345678904", new DateTimeOffset(new DateTime(2024, 6, 5, 9, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "192.168.2.102", "HPL Electric", "Active" },
                    { "LT24C00255", "Three Phase", 2L, "v4.5.1", "899110234567890123B", "404112345678902", new DateTimeOffset(new DateTime(2024, 3, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "192.168.1.102", "L&T Electrical", "Active" },
                    { "LT24I00419", "Three Phase CT", 5L, "v5.0.2-beta", "899110234567890123E", "404112345678905", new DateTimeOffset(new DateTime(2024, 1, 25, 15, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "192.168.1.103", "L&T Electrical", "Active" }
                });

            migrationBuilder.InsertData(
                table: "MeterReadings",
                columns: new[] { "Id", "Current", "EnergyConsumed", "MeterId", "PowerFactor", "ReadingDateTime", "Voltage" },
                values: new object[,]
                {
                    { 1, 5.2000000000000002, 1540.5, "GN24A00187", 0.97999999999999998, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 231.5 },
                    { 2, 5.5, 1715.8, "GN24A00187", 0.98999999999999999, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 229.80000000000001 },
                    { 3, 5.2999999999999998, 1898.2, "GN24A00187", 0.97999999999999998, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 230.09999999999999 },
                    { 4, 15.800000000000001, 8550.2000000000007, "LT24C00255", 0.94999999999999996, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 235.19999999999999 },
                    { 5, 16.100000000000001, 9460.7000000000007, "LT24C00255", 0.95999999999999996, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 234.90000000000001 },
                    { 6, 15.9, 10395.1, "LT24C00255", 0.94999999999999996, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 235.5 },
                    { 7, 6.0999999999999996, 1210.3, "GN24A00193", 0.98999999999999999, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 228.69999999999999 },
                    { 8, 6.4000000000000004, 1435.5, "GN24A00193", 0.98999999999999999, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 229.09999999999999 },
                    { 9, 6.2000000000000002, 1655.9000000000001, "GN24A00193", 0.97999999999999998, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 228.90000000000001 },
                    { 10, 4.5, 525.60000000000002, "HP24E00301", 0.98999999999999999, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 230.5 },
                    { 11, 4.7999999999999998, 680.10000000000002, "HP24E00301", 0.98999999999999999, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 231.19999999999999 },
                    { 12, 4.5999999999999996, 840.29999999999995, "HP24E00301", 0.98999999999999999, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 230.80000000000001 },
                    { 13, 55.200000000000003, 156234.79999999999, "LT24I00419", 0.92000000000000004, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 414.5 },
                    { 14, 58.100000000000001, 168456.10000000001, "LT24I00419", 0.93000000000000005, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 415.10000000000002 },
                    { 15, 56.5, 181050.39999999999, "LT24I00419", 0.92000000000000004, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 413.89999999999998 }
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
