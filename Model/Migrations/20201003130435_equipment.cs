using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class equipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentCategories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    image = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 25, nullable: false),
                    features = table.Column<string>(maxLength: 500, nullable: true),
                    purchasedPrice = table.Column<double>(nullable: false),
                    dayAdded = table.Column<DateTime>(nullable: false),
                    categoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentCategories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "EquipmentCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_categoryId",
                table: "Equipments",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "EquipmentCategories");
        }
    }
}
