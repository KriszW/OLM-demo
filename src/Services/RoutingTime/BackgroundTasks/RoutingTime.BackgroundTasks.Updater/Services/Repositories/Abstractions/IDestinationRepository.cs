using OLM.Services.RoutingTime.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface IDestinationRepository
    {
        Task<DateTime> GetLatestDate();

        Task UploadNewModels(IEnumerable<BundleModel> models);
    }
}
