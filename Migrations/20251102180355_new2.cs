using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1L, new DateTime(2025, 11, 2, 18, 3, 54, 520, DateTimeKind.Utc).AddTicks(674), "System", "priya.s@example.com", false, "Priya Sharma", 5, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543211", null, "Active", 1, null, null },
                    { 2L, new DateTime(2025, 11, 2, 18, 3, 54, 520, DateTimeKind.Utc).AddTicks(1282), "System", "rohan.k@example.com", false, "Rohan Kumar", 5, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543212", null, "Active", 2, null, null },
                    { 3L, new DateTime(2025, 11, 2, 18, 3, 54, 520, DateTimeKind.Utc).AddTicks(1284), "System", "vikram.s@example.com", false, "Vikram Singh", 6, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543213", null, "Active", 1, null, null },
                    { 4L, new DateTime(2025, 11, 2, 18, 3, 54, 520, DateTimeKind.Utc).AddTicks(1286), "System", "anjali.d@example.com", false, "Anjali Devi", 6, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543214", null, "Active", 5, null, null },
                    { 5L, new DateTime(2025, 11, 2, 18, 3, 54, 520, DateTimeKind.Utc).AddTicks(1287), "System", "contact@guptaindustries.com", false, "Gupta Industries", 5, "$2a$11$pXHOt/8COqUisMbyPjx7Euy2y3myYS9AukeD6jNo91NtYXmKy.k4W", "9876543215", null, "Active", 3, null, null }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MeterReadings",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "MeterSerialNo",
                keyValue: "SC23S00078");

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TariffDetails",
                keyColumn: "TariffDetailsId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "MeterSerialNo",
                keyValue: "GN24A00187");

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "MeterSerialNo",
                keyValue: "GN24A00193");

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "MeterSerialNo",
                keyValue: "HP24E00301");

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "MeterSerialNo",
                keyValue: "LT24C00255");

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "MeterSerialNo",
                keyValue: "LT24I00419");

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TariffSlabs",
                keyColumn: "TariffSlabId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TodRules",
                keyColumn: "TodRuleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Consumers",
                keyColumn: "ConsumerId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Consumers",
                keyColumn: "ConsumerId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Consumers",
                keyColumn: "ConsumerId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Consumers",
                keyColumn: "ConsumerId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Consumers",
                keyColumn: "ConsumerId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "OrgUnitId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "OrgUnitId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "OrgUnitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "OrgUnitId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "OrgUnitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "OrgUnitId",
                keyValue: 1);
        }
    }
}
