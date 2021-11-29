using Microsoft.EntityFrameworkCore;
using OLM.Services.Routing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Data
{
    public class RoutingDbContext : DbContext
    {
        public RoutingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RoutingModel> Routing { get; set; }
    }
}
