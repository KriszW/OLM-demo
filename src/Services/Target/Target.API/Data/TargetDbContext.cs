using Microsoft.EntityFrameworkCore;
using OLM.Services.Target.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Data
{
    public class TargetDbContext : DbContext
    {
        public TargetDbContext(DbContextOptions options) : base(options) { }

        public DbSet<WasteTargetDataModel> Targets { get; set; }
    }
}
