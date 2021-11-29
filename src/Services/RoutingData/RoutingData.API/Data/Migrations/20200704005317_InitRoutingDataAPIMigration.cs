using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.RoutingData.API.Migrations
{
    public partial class InitRoutingDataAPIMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BundleData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dimension = table.Column<string>(nullable: false),
                    AllLength = table.Column<double>(nullable: false),
                    FinishedDate = table.Column<DateTime>(nullable: false),
                    MachineName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleData", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundleData");
        }
    }
}
