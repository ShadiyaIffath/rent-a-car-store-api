using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class vehiclesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_VehicleTypes_typeId",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Inventories");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Vehicles");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_typeId",
                table: "Vehicles",
                newName: "IX_Vehicles_typeId");

            migrationBuilder.AlterColumn<int>(
                name: "typeId",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "model",
                table: "Vehicles",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "engine",
                table: "Vehicles",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "color",
                table: "Vehicles",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "carCode",
                table: "Vehicles",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "automatic",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypes_typeId",
                table: "Vehicles",
                column: "typeId",
                principalTable: "VehicleTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypes_typeId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Inventories");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_typeId",
                table: "Inventories",
                newName: "IX_Inventories_typeId");

            migrationBuilder.AlterColumn<int>(
                name: "typeId",
                table: "Inventories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "model",
                table: "Inventories",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "engine",
                table: "Inventories",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "color",
                table: "Inventories",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "carCode",
                table: "Inventories",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "automatic",
                table: "Inventories",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_VehicleTypes_typeId",
                table: "Inventories",
                column: "typeId",
                principalTable: "VehicleTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
