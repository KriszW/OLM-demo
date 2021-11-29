using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface IDestinationBundlesRepository
    {
        Task UploadBundles(IEnumerable<BundleModel> models);

        Task<DateTime> GetLatestBundleFinishedDate();
    }
}
