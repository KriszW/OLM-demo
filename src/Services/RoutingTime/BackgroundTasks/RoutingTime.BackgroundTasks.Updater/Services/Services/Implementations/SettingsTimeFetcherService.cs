using OLM.Services.RoutingTime.BackgroundTasks.Updater.Models;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Utilities.RoutingTime;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Services.Implementations
{
    public class SettingsTimeFetcherService : ISettingsTimeFetcherService
    {
        private readonly RoutingTimeSettings _routingTimeSettings;

        public SettingsTimeFetcherService(RoutingTimeSettings routingTimeSettings)
        {
            _routingTimeSettings = routingTimeSettings;
        }

        public IEnumerable<PauseModel> FetchPauseModelsForDay(DateTime date)
        {
            var day = date.Date;

            var allPausesModel = _routingTimeSettings.Pauses;

            var dayModels = allPausesModel.Where(m => m.Day == day.DayOfWeek);

            if (dayModels.Any() == true)
                return dayModels.Select(m => new PauseModel { Day = m.Day, MachineName = m.MachineName, WeekNumber = GetWeekNumber(date), Start = day.Add(m.StartOffset), End = day.Add(m.EndOffSet) });
            else
                return default;
        }

        public IEnumerable<ProductionTimeModel> FetchProdTimeModelsForDay(DateTime date)
        {
            var day = date.Date;

            var allProdTimeModel = _routingTimeSettings.ProdTimes;

            var dayModels = allProdTimeModel.Where(m => m.Day == day.DayOfWeek);

            if (dayModels.Any() == true)
                return dayModels.Select(m => new ProductionTimeModel { Day = m.Day, MachineName = m.MachineName, WeekNumber = GetWeekNumber(date), Start = day.Add(m.StartOffset), End = day.Add(m.EndOffSet) });
            else
                return default;
        }

        private int GetWeekNumber(DateTime date)
            => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }
}
