using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.RoutingData.API.Migrations
{
    public partial class AddBundleIDToBundleDataModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BundleID",
                table: "BundleData",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BundleID",
                table: "BundleData");
        }
    }
}
