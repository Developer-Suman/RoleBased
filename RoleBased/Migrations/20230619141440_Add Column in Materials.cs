using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleBased.Migrations
{
    public partial class AddColumninMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Materials");
        }
    }
}
