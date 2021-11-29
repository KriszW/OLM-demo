using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OLM.Services.Tram.API.Models;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Data
{
    public class TramDbContext : DbContext
    {
        public TramDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TramDataModel> Trams { get; set; }
        public DbSet<TramDimensionModel> Dimensions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var shiftConverter = new EnumToStringConverter<ShiftTypes>();

            modelBuilder
                .Entity<TramDataModel>()
                .Property(e => e.Shift)
                .HasConversion(shiftConverter);
                //.HasConversion(
                //    v => v.ToString(),
                //    v => (ShiftTypes)Enum.Parse(typeof(ShiftTypes), v));
        }
    }
}
