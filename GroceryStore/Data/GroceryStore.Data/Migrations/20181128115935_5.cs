using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryStore.Data.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Kind",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Kind",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
