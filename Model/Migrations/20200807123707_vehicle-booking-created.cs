using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class vehiclebookingcreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleBookings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startTime = table.Column<DateTime>(nullable: false),
                    endTime = table.Column<DateTime>(nullable: false),
                    confirmationCode = table.Column<string>(maxLength: 10, nullable: false),
                    totalCost = table.Column<double>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(maxLength: 10, nullable: false),
                    vehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBookings", x => x.id);
                    table.ForeignKey(
                        name: "FK_VehicleBookings_Vehicles_vehicleId",
                        column: x => x.vehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBookings_vehicleId",
                table: "VehicleBookings",
                column: "vehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleBookings");
        }
    }
}
