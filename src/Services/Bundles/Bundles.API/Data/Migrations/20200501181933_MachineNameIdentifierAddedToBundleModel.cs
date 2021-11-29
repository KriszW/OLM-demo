using Microsoft.EntityFrameworkCore.Migrations;

namespace Bundles.API.Migrations
{
    public partial class MachineNameIdentifierAddedToBundleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "Bundles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "Bundles");
        }
    }
}
