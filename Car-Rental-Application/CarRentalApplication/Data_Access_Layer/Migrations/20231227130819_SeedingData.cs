using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarName", "ColorName", "Description", "Maker", "ModelYear" },
                values: new object[] { 1, "Camry", "Silver", "A comfortable sedan", "Toyota", 2022 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarName", "ColorName", "Description", "Maker", "ModelYear" },
                values: new object[] { 2, "Civic", "Blue", "A reliable compact car", "Honda", 2022 });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "RentalId", "BrandName", "CarId", "CustomerName", "RentDate", "RentStatus", "ReturnDate", "UserId" },
                values: new object[] { 1, "Toyota", 1, "John Doe", new DateTime(2023, 12, 27, 18, 38, 19, 346, DateTimeKind.Local).AddTicks(4730), true, new DateTime(2024, 1, 27, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(3341), "475f2e1e-dd1e-4945-9575-9c5f8cd81dd3" });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "RentalId", "BrandName", "CarId", "CustomerName", "RentDate", "RentStatus", "ReturnDate", "UserId" },
                values: new object[] { 3, "Toyota", 1, "Bob Smith", new DateTime(2023, 12, 22, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(4095), false, new DateTime(2024, 1, 27, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(4098), "3248df4f-e683-4ca1-b731-5c46100742cc" });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "RentalId", "BrandName", "CarId", "CustomerName", "RentDate", "RentStatus", "ReturnDate", "UserId" },
                values: new object[] { 2, "Honda", 2, "Jane Doe", new DateTime(2023, 12, 29, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(3847), false, new DateTime(2024, 1, 27, 18, 38, 19, 347, DateTimeKind.Local).AddTicks(4077), "75cae0e9-0a1d-47f0-8199-712bc3484561" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "RentalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
