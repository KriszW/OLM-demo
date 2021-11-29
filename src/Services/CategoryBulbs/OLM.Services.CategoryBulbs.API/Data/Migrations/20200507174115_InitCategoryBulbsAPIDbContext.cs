using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.CategoryBulbs.API.Migrations
{
    public partial class InitCategoryBulbsAPIDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Itemnumber = table.Column<string>(nullable: false),
                    CategoryType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemNumbers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Itemnumber = table.Column<string>(nullable: false),
                    BundleItemnumbersModelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemNumbers_Bundles_BundleItemnumbersModelID",
                        column: x => x.BundleItemnumbersModelID,
                        principalTable: "Bundles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemNumbers_BundleItemnumbersModelID",
                table: "ItemNumbers",
                column: "BundleItemnumbersModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ItemNumbers");

            migrationBuilder.DropTable(
                name: "Bundles");
        }
    }
}
