using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class SeedeDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Rentals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 1,
                columns: new[] { "Cost", "RentDate", "ReturnDate" },
                values: new object[] { 30000, new DateTime(2023, 12, 27, 19, 30, 30, 110, DateTimeKind.Local).AddTicks(2062), new DateTime(2024, 1, 27, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(2937) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 2,
                columns: new[] { "Cost", "RentDate", "ReturnDate" },
                values: new object[] { 28000, new DateTime(2023, 12, 29, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4107), new DateTime(2024, 1, 27, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 3,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 22, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4231), new DateTime(2024, 1, 27, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4236) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Rentals");

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
    }
}
