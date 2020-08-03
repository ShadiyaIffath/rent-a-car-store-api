using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class vehiclev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "color",
                table: "Vehicles");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "Vehicles",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Vehicles");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "VehicleTypes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "Vehicles",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
