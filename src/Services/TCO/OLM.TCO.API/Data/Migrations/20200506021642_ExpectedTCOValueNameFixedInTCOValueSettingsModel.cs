using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class ExpectedTCOValueNameFixedInTCOValueSettingsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpecetedTCOValue",
                table: "TCOContansValues");

            migrationBuilder.AddColumn<double>(
                name: "ExpectedTCOValue",
                table: "TCOContansValues",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedTCOValue",
                table: "TCOContansValues");

            migrationBuilder.AddColumn<double>(
                name: "ExpecetedTCOValue",
                table: "TCOContansValues",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
