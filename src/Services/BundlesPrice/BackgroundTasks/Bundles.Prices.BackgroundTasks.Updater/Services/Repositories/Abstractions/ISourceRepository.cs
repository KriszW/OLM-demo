using OLM.Services.Bundles.Prices.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface ISourceRepository
    {
        Task<DateTime> GetDatetimeForBundle(string bundleID);

        Task<IEnumerable<BundleModel>> GetModelsFrom(DateTime date);
    }
}
