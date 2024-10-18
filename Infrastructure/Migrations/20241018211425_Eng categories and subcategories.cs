using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Engcategoriesandsubcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0d6c2e2a-0f2e-4f05-af8e-f2fc8ef3ac11"),
                column: "Name",
                value: "Other");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"),
                column: "Name",
                value: "Private");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e669fad2-79f6-4a65-bf80-8f6799663dbf"),
                column: "Name",
                value: "Business");

            migrationBuilder.UpdateData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("080de9db-ed11-4d44-ab6e-4931fbcce785"),
                column: "Name",
                value: "Boss");

            migrationBuilder.UpdateData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("b8fb7cf0-3658-4ddd-8992-5340d7e559b0"),
                column: "Name",
                value: "Client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0d6c2e2a-0f2e-4f05-af8e-f2fc8ef3ac11"),
                column: "Name",
                value: "Inny");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"),
                column: "Name",
                value: "Prywatny");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e669fad2-79f6-4a65-bf80-8f6799663dbf"),
                column: "Name",
                value: "Słubowy");

            migrationBuilder.UpdateData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("080de9db-ed11-4d44-ab6e-4931fbcce785"),
                column: "Name",
                value: "Szef");

            migrationBuilder.UpdateData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("b8fb7cf0-3658-4ddd-8992-5340d7e559b0"),
                column: "Name",
                value: "Klient");
        }
    }
}
