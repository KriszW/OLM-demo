using OLM.Services.RoutingTime.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface IProdTimeRepository
    {
        Task<bool> AnyDataFor(DateTime date);
        Task<bool> AnyDataBetween(DateTime start, DateTime end);

        Task Upload(IEnumerable<ProductionTimeModel> models);
    }
}
