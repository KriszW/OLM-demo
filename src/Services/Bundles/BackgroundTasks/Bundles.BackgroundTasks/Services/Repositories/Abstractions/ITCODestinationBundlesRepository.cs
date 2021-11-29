using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface ITCODestinationBundlesRepository
    {
        Task UploadBundles(IEnumerable<TCOBundleModel> models);

        Task<DateTime> GetLatestBundleFinishedDate();

        Task<bool> AlreadyUploaded(string bundleID);
    }
}
