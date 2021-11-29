﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OLM.Services.CategoryBulbs.API.Data;

namespace OLM.Services.CategoryBulbs.API.Migrations
{
    [DbContext(typeof(CategoryBulbsDbContext))]
    [Migration("20200507174115_InitCategoryBulbsAPIDbContext")]
    partial class InitCategoryBulbsAPIDbContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OLM.Services.CategoryBulbs.API.Models.BundleItemnumbersModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BundleID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Bundles");
                });

            modelBuilder.Entity("OLM.Services.CategoryBulbs.API.Models.ItemnumberCategoryModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Itemnumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OLM.Services.CategoryBulbs.API.Models.ItemnumberModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BundleItemnumbersModelID")
                        .HasColumnType("int");

                    b.Property<string>("Itemnumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BundleItemnumbersModelID");

                    b.ToTable("ItemNumbers");
                });

            modelBuilder.Entity("OLM.Services.CategoryBulbs.API.Models.ItemnumberModel", b =>
                {
                    b.HasOne("OLM.Services.CategoryBulbs.API.Models.BundleItemnumbersModel", null)
                        .WithMany("Models")
                        .HasForeignKey("BundleItemnumbersModelID");
                });
#pragma warning restore 612, 618
        }
    }
}
