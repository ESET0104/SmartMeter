using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMeterWeb.Migrations
{
    /// <inheritdoc />
    public partial class billingfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Billings",
                keyColumn: "BillId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Billings",
                keyColumn: "BillId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Billings",
                keyColumn: "BillId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Billings",
                keyColumn: "BillId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Billings",
                keyColumn: "BillId",
                keyValue: 5L);
        }
    }
}
