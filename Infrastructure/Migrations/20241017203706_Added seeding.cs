using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0d6c2e2a-0f2e-4f05-af8e-f2fc8ef3ac11"), "Inny" },
                    { new Guid("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"), "Prywatny" },
                    { new Guid("e669fad2-79f6-4a65-bf80-8f6799663dbf"), "Służbowy" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "IsDefault", "Name" },
                values: new object[,]
                {
                    { new Guid("080de9db-ed11-4d44-ab6e-4931fbcce785"), new Guid("e669fad2-79f6-4a65-bf80-8f6799663dbf"), true, "Szef" },
                    { new Guid("b8fb7cf0-3658-4ddd-8992-5340d7e559b0"), new Guid("e669fad2-79f6-4a65-bf80-8f6799663dbf"), true, "Klient" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0d6c2e2a-0f2e-4f05-af8e-f2fc8ef3ac11"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e669fad2-79f6-4a65-bf80-8f6799663dbf"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("080de9db-ed11-4d44-ab6e-4931fbcce785"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("b8fb7cf0-3658-4ddd-8992-5340d7e559b0"));
        }
    }
}
