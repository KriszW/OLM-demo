using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.Tram.API.Migrations
{
    public partial class InitTramAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dimension = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Shift = table.Column<string>(nullable: false),
                    DimensionID = table.Column<int>(nullable: false),
                    NumberOfLamella = table.Column<int>(nullable: false),
                    NumberOfTrams = table.Column<int>(nullable: false),
                    MachineID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trams_Dimensions_DimensionID",
                        column: x => x.DimensionID,
                        principalTable: "Dimensions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trams_DimensionID",
                table: "Trams",
                column: "DimensionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trams");

            migrationBuilder.DropTable(
                name: "Dimensions");
        }
    }
}
