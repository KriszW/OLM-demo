using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class BundleIDAddedForTCOData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BundleID",
                table: "TCOData",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Bundles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BundleID",
                table: "TCOData");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Bundles",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
