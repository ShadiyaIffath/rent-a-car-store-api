using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class updatevehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carCode",
                table: "Vehicles");

            migrationBuilder.AlterColumn<float>(
                name: "fuelConsumption",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "engineCapacity",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "make",
                table: "Vehicles",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "make",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "fuelConsumption",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "engineCapacity",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<string>(
                name: "carCode",
                table: "Vehicles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
