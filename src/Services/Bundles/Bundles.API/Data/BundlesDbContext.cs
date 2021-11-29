using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Data
{
    public class BundlesDbContext : DbContext
    {
        public BundlesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BundleModel> Bundles { get; set; }
    }
}
