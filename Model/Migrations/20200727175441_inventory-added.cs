using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class inventoryadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(maxLength: 25, nullable: false),
                    pricePerDay = table.Column<double>(nullable: false),
                    image = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(nullable: false),
                    dayAdded = table.Column<DateTime>(nullable: false),
                    dayRemoved = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    carCode = table.Column<string>(maxLength: 10, nullable: true),
                    color = table.Column<string>(maxLength: 15, nullable: true),
                    model = table.Column<string>(maxLength: 25, nullable: true),
                    engine = table.Column<string>(maxLength: 10, nullable: true),
                    automatic = table.Column<bool>(nullable: true),
                    typeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.id);
                    table.ForeignKey(
                        name: "FK_Inventories_VehicleTypes_typeId",
                        column: x => x.typeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_typeId",
                table: "Inventories",
                column: "typeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
