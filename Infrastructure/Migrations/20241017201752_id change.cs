using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class idchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationData_Users_UserId",
                table: "AuthenticationData");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AuthenticationData",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Contacts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationData_Users_Id",
                table: "AuthenticationData",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationData_Users_Id",
                table: "AuthenticationData");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AuthenticationData",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationData_Users_UserId",
                table: "AuthenticationData",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
