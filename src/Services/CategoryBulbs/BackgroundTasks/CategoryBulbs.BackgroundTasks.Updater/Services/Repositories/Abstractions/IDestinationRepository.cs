using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface IDestinationRepository
    {
        Task<string> GetLatestBundleID();

        Task UploadNewModels(IEnumerable<DestinationBundleModel> bundles);
    }
}
