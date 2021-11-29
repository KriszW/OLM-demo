using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.Prices.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Data
{
    public class BundlePriceDbContext : DbContext
    {
        public BundlePriceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BundlePriceModel> BundlePrices { get; set; }

        public DbSet<RawBundlesModel> Bundles { get; set; }
    }
}
