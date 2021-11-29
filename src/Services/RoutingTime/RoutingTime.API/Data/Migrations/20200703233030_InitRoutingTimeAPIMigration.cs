using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.RoutingTime.API.Migrations
{
    public partial class InitRoutingTimeAPIMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dimension = table.Column<string>(nullable: false),
                    FinishedDate = table.Column<DateTime>(nullable: false),
                    MachineName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pauses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Day = table.Column<string>(nullable: false),
                    WeekNumber = table.Column<int>(nullable: false),
                    MachineName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pauses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductionTimes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Day = table.Column<string>(nullable: false),
                    WeekNumber = table.Column<int>(nullable: false),
                    MachineName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionTimes", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Pauses");

            migrationBuilder.DropTable(
                name: "ProductionTimes");
        }
    }
}
