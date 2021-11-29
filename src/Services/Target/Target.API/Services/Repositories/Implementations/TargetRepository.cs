using Microsoft.EntityFrameworkCore;
using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Services.Repositories.Implementations
{
    public class TargetRepository : ITargetRepository
    {
        private readonly TargetDbContext _dbContext;

        public TargetRepository(TargetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<WasteTargetDataModel> GetByDimension(string dimension)
            => _dbContext.Targets.FirstOrDefaultAsync(m => m.Dimension == dimension);

        public Task<List<WasteTargetDataModel>> GetByDimension(IEnumerable<string> dimensions)
            => _dbContext.Targets.Where(m => dimensions.Any(d => d == m.Dimension)).ToListAsync();
    }
}
