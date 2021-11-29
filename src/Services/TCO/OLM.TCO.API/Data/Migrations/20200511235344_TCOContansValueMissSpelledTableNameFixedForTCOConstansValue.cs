using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.TCO.API.Migrations
{
    public partial class TCOContansValueMissSpelledTableNameFixedForTCOConstansValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TCOContansValues",
                table: "TCOContansValues");

            migrationBuilder.RenameTable(
                name: "TCOContansValues",
                newName: "TCOConstansValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TCOConstansValues",
                table: "TCOConstansValues",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TCOConstansValues",
                table: "TCOConstansValues");

            migrationBuilder.RenameTable(
                name: "TCOConstansValues",
                newName: "TCOContansValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TCOContansValues",
                table: "TCOContansValues",
                column: "ID");
        }
    }
}
