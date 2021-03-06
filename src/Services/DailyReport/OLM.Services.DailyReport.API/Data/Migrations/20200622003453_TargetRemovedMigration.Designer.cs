// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OLM.Services.DailyReport.API.Data;

namespace OLM.Services.DailyReport.API.Migrations
{
    [DbContext(typeof(DailyReportDbContext))]
    [Migration("20200622003453_TargetRemovedMigration")]
    partial class TargetRemovedMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OLM.Services.DailyReport.API.Models.DailyReportDataModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dimension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<double>("LengthOfFS")
                        .HasColumnType("float");

                    b.Property<double>("LengthOfWaste")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("ReportData");
                });
#pragma warning restore 612, 618
        }
    }
}
