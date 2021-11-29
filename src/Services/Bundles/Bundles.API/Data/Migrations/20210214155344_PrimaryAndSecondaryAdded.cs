using Microsoft.EntityFrameworkCore.Migrations;

namespace Bundles.API.Migrations
{
    public partial class PrimaryAndSecondaryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Primary",
                table: "Bundles",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Secondary",
                table: "Bundles",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Primary",
                table: "Bundles");

            migrationBuilder.DropColumn(
                name: "Secondary",
                table: "Bundles");
        }
    }
}
