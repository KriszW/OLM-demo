using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OLM.Services.RoutingTime.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Data
{
    public class RoutingTimeDbContext : DbContext
    {
        public RoutingTimeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PauseModel> Pauses { get; set; }

        public DbSet<ProductionTimeModel> ProductionTimes { get; set; }

        public DbSet<BundleModel> Bundles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dayConverter = new EnumToStringConverter<DayOfWeek>();

            modelBuilder
                .Entity<PauseModel>()
                .Property(e => e.Day)
                .HasConversion(dayConverter);

            modelBuilder
                .Entity<ProductionTimeModel>()
                .Property(e => e.Day)
                .HasConversion(dayConverter);
        }
    }
}
