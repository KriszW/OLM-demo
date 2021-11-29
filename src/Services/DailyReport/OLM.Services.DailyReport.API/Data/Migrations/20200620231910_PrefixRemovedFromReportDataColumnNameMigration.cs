using Microsoft.EntityFrameworkCore.Migrations;

namespace OLM.Services.DailyReport.API.Migrations
{
    public partial class PrefixRemovedFromReportDataColumnNameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllLength",
                table: "ReportData");

            migrationBuilder.DropColumn(
                name: "AllLengthOfFS",
                table: "ReportData");

            migrationBuilder.DropColumn(
                name: "AllLengthOfWaste",
                table: "ReportData");

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "ReportData",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LengthOfFS",
                table: "ReportData",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LengthOfWaste",
                table: "ReportData",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "ReportData");

            migrationBuilder.DropColumn(
                name: "LengthOfFS",
                table: "ReportData");

            migrationBuilder.DropColumn(
                name: "LengthOfWaste",
                table: "ReportData");

            migrationBuilder.AddColumn<double>(
                name: "AllLength",
                table: "ReportData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AllLengthOfFS",
                table: "ReportData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AllLengthOfWaste",
                table: "ReportData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
