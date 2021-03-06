// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OLM.Services.RoutingData.API.Data;

namespace OLM.Services.RoutingData.API.Migrations
{
    [DbContext(typeof(RoutingDataDbContext))]
    [Migration("20200704005317_InitRoutingDataAPIMigration")]
    partial class InitRoutingDataAPIMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OLM.Services.RoutingData.API.Models.BundleDataModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AllLength")
                        .HasColumnType("float");

                    b.Property<string>("Dimension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("BundleData");
                });
#pragma warning restore 612, 618
        }
    }
}
