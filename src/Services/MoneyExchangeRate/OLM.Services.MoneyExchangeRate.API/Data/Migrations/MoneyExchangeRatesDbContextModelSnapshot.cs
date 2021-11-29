﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OLM.Services.MoneyExchangeRate.API.Data;

namespace OLM.Services.MoneyExchangeRate.API.Migrations
{
    [DbContext(typeof(MoneyExchangeRatesDbContext))]
    partial class MoneyExchangeRatesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OLM.Services.MoneyExchangeRate.API.Models.CurrencyModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ISOCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("OLM.Services.MoneyExchangeRate.API.Models.ExchangeRateModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrencyModelID")
                        .HasColumnType("int");

                    b.Property<string>("DestISOCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("CurrencyModelID");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("OLM.Services.MoneyExchangeRate.API.Models.ExchangeRateModel", b =>
                {
                    b.HasOne("OLM.Services.MoneyExchangeRate.API.Models.CurrencyModel", null)
                        .WithMany("Rates")
                        .HasForeignKey("CurrencyModelID");
                });

            modelBuilder.Entity("OLM.Services.MoneyExchangeRate.API.Models.CurrencyModel", b =>
                {
                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}