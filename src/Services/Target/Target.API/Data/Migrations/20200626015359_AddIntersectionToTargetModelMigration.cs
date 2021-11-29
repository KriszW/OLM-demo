using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.Target.API.Migrations
{
    public partial class AddIntersectionToTargetModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Intersection",
                table: "Targets",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intersection",
                table: "Targets");
        }
    }
}
