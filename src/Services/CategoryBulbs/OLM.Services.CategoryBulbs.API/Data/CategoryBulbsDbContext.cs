using Microsoft.EntityFrameworkCore;
using OLM.Services.CategoryBulbs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Data
{
    public class CategoryBulbsDbContext : DbContext
    {
        public CategoryBulbsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BundleItemnumbersModel> Bundles { get; set; }
        public DbSet<ItemnumberCategoryModel> Categories { get; set; }
        public DbSet<ItemnumberModel> ItemNumbers { get; set; }
    }
}
