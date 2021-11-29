using Microsoft.EntityFrameworkCore;
using OLM.Services.TCO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Data
{
    public class TCODataDbContext : DbContext
    {
        public TCODataDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TCODataModel> TCOData { get; set; }

        public DbSet<TCOValueSettingsModel> TCOConstansValues { get; set; }
    }
}
