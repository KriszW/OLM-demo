using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingData.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Data
{
    public class RoutingDataDbContext : DbContext
    {
        public RoutingDataDbContext(DbContextOptions options) : base(options) { }

        public DbSet<BundleDataModel> BundleData { get; set; }
    }
}
