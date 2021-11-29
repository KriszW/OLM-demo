using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.Bundles.Prices.API.Migrations
{
    public partial class CurrencyAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BundlePrices",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BundlePrices");
        }
    }
}
