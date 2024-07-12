using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class ReturnRequestAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestReturn",
                table: "Rentals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 1,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 28, 20, 36, 27, 138, DateTimeKind.Local).AddTicks(6104), new DateTime(2024, 1, 28, 20, 36, 27, 139, DateTimeKind.Local).AddTicks(9222) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 2,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 30, 20, 36, 27, 140, DateTimeKind.Local).AddTicks(428), new DateTime(2024, 1, 28, 20, 36, 27, 140, DateTimeKind.Local).AddTicks(599) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 3,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 23, 20, 36, 27, 140, DateTimeKind.Local).AddTicks(634), new DateTime(2024, 1, 28, 20, 36, 27, 140, DateTimeKind.Local).AddTicks(638) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestReturn",
                table: "Rentals");

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 1,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 27, 19, 30, 30, 110, DateTimeKind.Local).AddTicks(2062), new DateTime(2024, 1, 27, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(2937) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 2,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 29, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4107), new DateTime(2024, 1, 27, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 3,
                columns: new[] { "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2023, 12, 22, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4231), new DateTime(2024, 1, 27, 19, 30, 30, 111, DateTimeKind.Local).AddTicks(4236) });
        }
    }
}
