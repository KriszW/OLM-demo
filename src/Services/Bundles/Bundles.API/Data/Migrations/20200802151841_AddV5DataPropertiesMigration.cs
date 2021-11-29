using Microsoft.EntityFrameworkCore.Migrations;

namespace Bundles.API.Migrations
{
    public partial class AddV5DataPropertiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "Bundles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "Bundles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SawmillName",
                table: "Bundles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "Bundles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "Bundles");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Bundles");

            migrationBuilder.DropColumn(
                name: "SawmillName",
                table: "Bundles");

            migrationBuilder.DropColumn(
                name: "VendorName",
                table: "Bundles");
        }
    }
}
