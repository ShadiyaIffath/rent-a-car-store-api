using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class vehicleentityfinalise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "engineCapacity",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fuelConsumption",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "engineCapacity",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "fuelConsumption",
                table: "Vehicles");
        }
    }
}
