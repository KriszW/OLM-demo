using OLM.Services.RoutingData.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface ISourceRepository
    {
        Task<IEnumerable<RoutingDataModel>> GetModelsFrom(DateTime date);
    }
}
