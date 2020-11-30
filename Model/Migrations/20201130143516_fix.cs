using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "additionalIdentitfication",
                table: "Accounts");

            migrationBuilder.AddColumn<byte[]>(
                name: "additionalIdentification",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "additionalIdentification",
                table: "Accounts");

            migrationBuilder.AddColumn<byte[]>(
                name: "additionalIdentitfication",
                table: "Accounts",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
