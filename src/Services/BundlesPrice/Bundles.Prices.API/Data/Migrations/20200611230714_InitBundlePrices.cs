using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.Bundles.Prices.API.Migrations
{
    public partial class InitBundlePrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BundlePrices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawMaterialItemNumber = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    VendorID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundlePrices", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundlePrices");
        }
    }
}
