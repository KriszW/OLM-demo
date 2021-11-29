using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.DailyReport.API.Migrations
{
    public partial class InitDailyReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Dimension = table.Column<string>(nullable: false),
                    AllLength = table.Column<double>(nullable: false),
                    AllLengthOfWaste = table.Column<double>(nullable: false),
                    AllLengthOfFS = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dimension = table.Column<string>(nullable: false),
                    Target = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportData");

            migrationBuilder.DropTable(
                name: "Targets");
        }
    }
}
