using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class DimensionChangedToItemNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "TCOData");

            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "TCOContansValues");

            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "Bundles");

            migrationBuilder.AddColumn<string>(
                name: "SplitWoodItemNumber",
                table: "TCOData",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SplitWoodItemNumber",
                table: "TCOContansValues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SplitWoodItemNumber",
                table: "Bundles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SplitWoodItemNumber",
                table: "TCOData");

            migrationBuilder.DropColumn(
                name: "SplitWoodItemNumber",
                table: "TCOContansValues");

            migrationBuilder.DropColumn(
                name: "SplitWoodItemNumber",
                table: "Bundles");

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "TCOData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "TCOContansValues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "Bundles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
