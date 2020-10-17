using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class equipmentCategoryprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "EquipmentCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "EquipmentCategories");
        }
    }
}
