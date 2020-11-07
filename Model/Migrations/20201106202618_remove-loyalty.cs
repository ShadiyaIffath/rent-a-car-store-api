using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class removeloyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loyal",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "licenseId",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "licenseId",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "loyal",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
