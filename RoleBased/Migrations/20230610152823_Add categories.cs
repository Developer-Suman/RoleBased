using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleBased.Migrations
{
    public partial class Addcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatagoriesId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatagoriesId",
                table: "Materials");
        }
    }
}
