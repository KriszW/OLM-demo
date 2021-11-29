using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class InitTCOAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(nullable: false),
                    Dimension = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TCOData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Volume = table.Column<double>(nullable: false),
                    Primary = table.Column<double>(nullable: false),
                    Secondary = table.Column<double>(nullable: false),
                    Dimension = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCOData", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "TCOData");
        }
    }
}
