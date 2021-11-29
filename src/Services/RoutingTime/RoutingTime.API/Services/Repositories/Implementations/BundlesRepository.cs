using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Repositories.Implementations
{
    public class BundlesRepository : IBundlesRepository
    {
        private readonly RoutingTimeDbContext _dbContext;

        public BundlesRepository(RoutingTimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<BundleModel>> FetchAll(string machineName, DateTime start, DateTime end)
            => _dbContext.Bundles.Where(m => m.MachineName == machineName && m.FinishedDate >= start && m.FinishedDate <= end).ToListAsync();
    }
}
