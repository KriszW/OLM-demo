using Microsoft.EntityFrameworkCore;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Repositories.Implementations
{
    public class TCODataRepository : ITCODataRepository
    {
        private TCODataDbContext _dbContext;

        public TCODataRepository(TCODataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TCODataModel> GetByBundleID(string bundleID) => _dbContext.TCOData.FirstOrDefaultAsync(td => td.BundleID == bundleID);

        public Task<List<TCODataModel>> GetByBundleIDs(IEnumerable<string> bundleIDs) => _dbContext.TCOData.Where(td => bundleIDs.Any(b => b == td.BundleID)).ToListAsync();
    }
}
