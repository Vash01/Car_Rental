using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class CostAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 1,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 27, 19, 29, 4, 34, DateTimeKind.Local).AddTicks(9422), new DateTime(2024, 1, 27, 19, 29, 4, 36, DateTimeKind.Local).AddTicks(2231) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 2,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 29, 19, 29, 4, 36, DateTimeKind.Local).AddTicks(3290), new DateTime(2024, 1, 27, 19, 29, 4, 36, DateTimeKind.Local).AddTicks(3388) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 3,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 22, 19, 29, 4, 36, DateTimeKind.Local).AddTicks(3500), new DateTime(2024, 1, 27, 19, 29, 4, 36, DateTimeKind.Local).AddTicks(3505) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 1,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 27, 18, 38, 19, 346, DateTimeKind.Local).AddTicks(4730), new DateTime(2024, 1, 27, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(3341) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 2,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 29, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(3847), new DateTime(2024, 1, 27, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(4077) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 3,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 22, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(4095), new DateTime(2024, 1, 27, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(4098) });
        }
    }
}
