using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class statusupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "EquipmentBookings");

            migrationBuilder.AddColumn<bool>(
                name: "loyal",
                table: "Accounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loyal",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "EquipmentBookings",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
