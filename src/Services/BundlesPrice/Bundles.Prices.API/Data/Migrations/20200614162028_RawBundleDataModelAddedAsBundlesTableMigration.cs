using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.Bundles.Prices.API.Migrations
{
    public partial class RawBundleDataModelAddedAsBundlesTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNumber = table.Column<string>(nullable: false),
                    VendorID = table.Column<string>(nullable: false),
                    BundleID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bundles");
        }
    }
}
