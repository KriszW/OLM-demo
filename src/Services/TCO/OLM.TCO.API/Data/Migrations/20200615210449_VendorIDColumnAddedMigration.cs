using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class VendorIDColumnAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VendorID",
                table: "TCOData",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorID",
                table: "TCOData");
        }
    }
}
