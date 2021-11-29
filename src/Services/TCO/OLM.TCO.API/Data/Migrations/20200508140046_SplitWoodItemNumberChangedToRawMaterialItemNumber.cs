using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class SplitWoodItemNumberChangedToRawMaterialItemNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "RawMaterialItemNumber",
                table: "TCOData",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RawMaterialItemNumber",
                table: "TCOContansValues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawMaterialItemNumber",
                table: "Bundles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawMaterialItemNumber",
                table: "TCOData");

            migrationBuilder.DropColumn(
                name: "RawMaterialItemNumber",
                table: "TCOContansValues");

            migrationBuilder.DropColumn(
                name: "RawMaterialItemNumber",
                table: "Bundles");

            migrationBuilder.AddColumn<string>(
                name: "SplitWoodItemNumber",
                table: "TCOData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SplitWoodItemNumber",
                table: "TCOContansValues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SplitWoodItemNumber",
                table: "Bundles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
