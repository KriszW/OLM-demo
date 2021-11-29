using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Models;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Implementations.Bundle
{
    public class BundlesSumRepository : IBundlesSumRepository
    {
        private BundlesDbContext _dbContext;
        private IStartDateProvider _startDateProvider;

        public BundlesSumRepository(BundlesDbContext dbContext, IStartDateProvider startDateProvider)
        {
            _dbContext = dbContext;
            _startDateProvider = startDateProvider;
        }

        public Task<List<BundleModel>> GetDailySumBundles()
        {
            var date = _startDateProvider.GetStartDateForDay();

            return FetchData(date);
        }

        public Task<List<BundleModel>> GetWeeklySumBundles()
        {
            var date = _startDateProvider.GetStartDateForWeek();

            return FetchData(date);
        }

        private Task<List<BundleModel>> FetchData(DateTime start) => _dbContext.Bundles.Where(b => b.FinishedDate > start).ToListAsync();
    }
}
