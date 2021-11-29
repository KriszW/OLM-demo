using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Models;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Shared.Models.Bundles.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle
{
    public class BundleRepository : IBundleRepository
    {
        private BundlesDbContext _dbContext;
        private IStartDateProvider _startDateProvider;

        public BundleRepository(BundlesDbContext dbContext, IStartDateProvider startDateProvider)
        {
            _dbContext = dbContext;
            _startDateProvider = startDateProvider;
        }

        public Task<BundleModel> GetLatestForMachine(string machineName)
            => _dbContext.Bundles.Where(b => b.MachineName == machineName).OrderByDescending(b => b.FinishedDate).Take(1).FirstOrDefaultAsync();

        public Task<List<BundleModel>> GetDailyBundlesForMachine(string machineName)
        {
            var startOfDate = _startDateProvider.GetStartDateForDay();

            return BuildDataFetcher(machineName, startOfDate);
        }

        public Task<List<BundleModel>> GetWeeklyBundlesForMachine(string machineName)
        {
            var startOfDate = _startDateProvider.GetStartDateForWeek();

            return BuildDataFetcher(machineName, startOfDate);
        }

        private Task<List<BundleModel>> BuildDataFetcher(string machineName, DateTime startOfDate)
            => _dbContext.Bundles.Where(b => b.MachineName == machineName && b.FinishedDate >= startOfDate).ToListAsync();

        public Task<List<BundleModel>> GetBundles(DateTime from, DateTime to)
            => _dbContext.Bundles.Where(b => b.FinishedDate > from && b.FinishedDate < to).ToListAsync();
    }
}
