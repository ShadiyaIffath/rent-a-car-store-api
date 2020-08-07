using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class bookingaccountConnected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "accountId",
                table: "VehicleBookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBookings_accountId",
                table: "VehicleBookings",
                column: "accountId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleBookings_Accounts_accountId",
                table: "VehicleBookings",
                column: "accountId",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleBookings_Accounts_accountId",
                table: "VehicleBookings");

            migrationBuilder.DropIndex(
                name: "IX_VehicleBookings_accountId",
                table: "VehicleBookings");

            migrationBuilder.DropColumn(
                name: "accountId",
                table: "VehicleBookings");
        }
    }
}
