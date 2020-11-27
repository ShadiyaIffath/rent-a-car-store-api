using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class updatecarRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRatings",
                table: "CarRatings");

            migrationBuilder.DropColumn(
                name: "AdvertId",
                table: "CarRatings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CarRatings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRatings",
                table: "CarRatings",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRatings",
                table: "CarRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarRatings");

            migrationBuilder.AddColumn<int>(
                name: "AdvertId",
                table: "CarRatings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRatings",
                table: "CarRatings",
                column: "AdvertId");
        }
    }
}
