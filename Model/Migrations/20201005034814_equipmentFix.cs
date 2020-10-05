using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class equipmentFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_EquipmentCategories_categoryId",
                table: "Equipments");

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "Equipments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_EquipmentCategories_categoryId",
                table: "Equipments",
                column: "categoryId",
                principalTable: "EquipmentCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_EquipmentCategories_categoryId",
                table: "Equipments");

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "Equipments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_EquipmentCategories_categoryId",
                table: "Equipments",
                column: "categoryId",
                principalTable: "EquipmentCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
