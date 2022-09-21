using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoK_Catalog.Migrations
{
    public partial class Add_Quantity_in_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quaintity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quaintity",
                table: "Products");
        }
    }
}
