using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.API.Data;
using OLM.Services.Bundles.API.Models;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Machine;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Implementations.Machine
{
    public class MachineBundlesRepository : IMachineBundlesRepository
    {
        private readonly BundlesDbContext _dbContext;
        private readonly IStartDateProvider _startDateProvider;

        public MachineBundlesRepository(BundlesDbContext dbContext, IStartDateProvider startDateProvider)
        {
            _dbContext = dbContext;
            _startDateProvider = startDateProvider;
        }

        public async Task<SummarizedBundlesForMachineDataViewModel> GetDailySummarizedData(string machineName)
        {
            var date = _startDateProvider.GetStartDateForDay();

            var bundles = _dbContext.Bundles.Where(b => b.FinishedDate >= date && b.MachineName == machineName);

            if (bundles.Any() == true)
            {
                var baseModel = await SummarizeBundles(bundles);

                return new SummarizedBundlesForMachineDataViewModel(baseModel.AllInput,
                                                                    baseModel.AllProduced,
                                                                    baseModel.AllFS,
                                                                    baseModel.AVGWastePercentage,
                                                                    machineName,
                                                                    baseModel.BundleIDs);
            }
            else
            {
                return default;
            }
        }

        public async Task<SummarizedBundlesForMachineDataViewModel> GetWeeklySummarizedData(string machineName)
        {
            var date = _startDateProvider.GetStartDateForWeek();

            var bundles = _dbContext.Bundles.Where(b => b.FinishedDate >= date && b.MachineName == machineName);

            if (bundles.Any() == true)
            {
                var baseModel = await SummarizeBundles(bundles);

                return new SummarizedBundlesForMachineDataViewModel(baseModel.AllInput,
                                                                    baseModel.AllProduced,
                                                                    baseModel.AllFS,
                                                                    baseModel.AVGWastePercentage,
                                                                    machineName,
                                                                    baseModel.BundleIDs);
            }
            else
            {
                return default;
            }
        }

        private async Task<SummarizedDataForMachinesViewModel> SummarizeBundles(IQueryable<BundleModel> bundles)
        {
            var allInput = await bundles.SumAsync(b => b.Input);
            var allProduced = await bundles.SumAsync(b => b.Produced);
            var allFS = await bundles.SumAsync(b => b.FS);
            var avgWaste = await bundles.AverageAsync(b => b.Waste / b.Input);


            return new SummarizedDataForMachinesViewModel(allInput,
                                                          allProduced,
                                                          allFS,
                                                          avgWaste,
                                                          bundles.Select(b => b.BundleID));
        }
    }
}
