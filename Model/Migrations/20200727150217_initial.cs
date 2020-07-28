using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AccountTypes",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        type = table.Column<string>(maxLength: 12, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AccountTypes", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Accounts",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        email = table.Column<string>(maxLength: 150, nullable: false),
            //        password = table.Column<string>(maxLength: 100, nullable: false),
            //        firstName = table.Column<string>(maxLength: 100, nullable: false),
            //        lastName = table.Column<string>(maxLength: 100, nullable: false),
            //        dob = table.Column<DateTime>(nullable: false),
            //        phone = table.Column<int>(maxLength: 10, nullable: false),
            //        drivingLicense = table.Column<byte[]>(nullable: true),
            //        additionalIdentitfication = table.Column<byte[]>(nullable: true),
            //        active = table.Column<bool>(nullable: false),
            //        activatedDate = table.Column<DateTime>(nullable: false),
            //        typeId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Accounts", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_Accounts_AccountTypes_typeId",
            //            column: x => x.typeId,
            //            principalTable: "AccountTypes",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Accounts_typeId",
            //    table: "Accounts",
            //    column: "typeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");
        }
    }
}
