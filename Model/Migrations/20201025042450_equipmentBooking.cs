using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class equipmentBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentBookings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startTime = table.Column<DateTime>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(maxLength: 10, nullable: false),
                    equipmentId = table.Column<int>(nullable: false),
                    vehicleBookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentBookings", x => x.id);
                    table.ForeignKey(
                        name: "FK_EquipmentBookings_Equipments_equipmentId",
                        column: x => x.equipmentId,
                        principalTable: "Equipments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentBookings_VehicleBookings_vehicleBookingId",
                        column: x => x.vehicleBookingId,
                        principalTable: "VehicleBookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBookings_equipmentId",
                table: "EquipmentBookings",
                column: "equipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBookings_vehicleBookingId",
                table: "EquipmentBookings",
                column: "vehicleBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentBookings");
        }
    }
}
