using Microsoft.EntityFrameworkCore.Migrations;

namespace Bundles.API.Migrations
{
    public partial class InitBundles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleID = table.Column<string>(nullable: false),
                    Input = table.Column<double>(nullable: false),
                    Produced = table.Column<double>(nullable: false),
                    FS = table.Column<double>(nullable: false),
                    Waste = table.Column<double>(nullable: false)
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
