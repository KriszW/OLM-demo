using OLM.Services.RoutingTime.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Abstractions
{
    public interface ISettingsTimeFetcherService
    {
        IEnumerable<PauseModel> FetchPauseModelsForDay(DateTime date);
        IEnumerable<ProductionTimeModel> FetchProdTimeModelsForDay(DateTime date);
    }
}
