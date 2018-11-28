using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryStore.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "OrderProducts",
                newName: "ProductWeight");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfOrdering",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderProducts",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductPicture",
                table: "OrderProducts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfOrdering",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "ProductWeight",
                table: "OrderProducts",
                newName: "Weight");
        }
    }
}
