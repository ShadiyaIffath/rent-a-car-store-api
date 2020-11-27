using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class carrating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarRatings",
                columns: table => new
                {
                    AdvertId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarCategory = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    RatePerMonth = table.Column<float>(nullable: false),
                    RatePerWeek = table.Column<float>(nullable: false),
                    Milleage = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRatings", x => x.AdvertId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarRatings");
        }
    }
}
