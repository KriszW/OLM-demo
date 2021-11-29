using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bundles.API.Migrations
{
    public partial class FinishedDateAddedToBundleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "Bundles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "Bundles");
        }
    }
}
