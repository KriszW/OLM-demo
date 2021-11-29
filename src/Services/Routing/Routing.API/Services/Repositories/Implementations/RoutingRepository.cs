using Microsoft.EntityFrameworkCore;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Repositories.Implementations
{
    public class RoutingRepository : IRoutingRepository
    {
        private readonly RoutingDbContext _dbContext;

        public RoutingRepository(RoutingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<RoutingModel>> Fetch(IEnumerable<string> dimension)
            => _dbContext.Routing.Where(m => dimension.Any(d => d == m.Dimension)).ToListAsync();
    }
}
