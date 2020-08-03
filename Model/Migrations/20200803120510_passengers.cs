using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class passengers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "passengers",
                table: "VehicleTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passengers",
                table: "VehicleTypes");
        }
    }
}
