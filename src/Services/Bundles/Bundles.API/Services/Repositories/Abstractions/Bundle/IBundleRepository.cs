using OLM.Services.Bundles.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle
{
    public interface IBundleRepository
    {
        Task<List<BundleModel>> GetBundles(DateTime from, DateTime to);
        Task<BundleModel> GetLatestForMachine(string machineName);
        Task<List<BundleModel>> GetDailyBundlesForMachine(string machineName);
        Task<List<BundleModel>> GetWeeklyBundlesForMachine(string machineName);
    }
}
