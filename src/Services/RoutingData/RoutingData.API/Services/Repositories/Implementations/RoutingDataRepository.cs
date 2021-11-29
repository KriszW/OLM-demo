using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingData.API.Data;
using OLM.Services.RoutingData.API.Models;
using OLM.Services.RoutingData.API.Services.Repositories.Abstractions;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Services.Repositories.Implementations
{
    public class RoutingDataRepository : IRoutingDataRepository
    {
        private readonly RoutingDataDbContext _dbContext;

        public RoutingDataRepository(RoutingDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<BundleDataModel>> FetchData(FetchRoutingDataRequestViewModel model)
            => _dbContext.BundleData.Where(m => m.MachineName == model.MachineName && m.FinishedDate >= model.Start && m.FinishedDate <= model.End).ToListAsync();
    }
}
