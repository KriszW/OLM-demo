using OLM.Services.DailyReport.API.Services.Repositories.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Daily;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weekly;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.Weekly
{
    public class WeeklyReportFetchService : IWeeklyReportFetchService
    {
        private readonly IWeeklyReportDataAggregatorService _weeklyReportDataAggregatorService;

        public WeeklyReportFetchService(IWeeklyReportDataAggregatorService weeklyReportDataAggregatorService)
        {
            _weeklyReportDataAggregatorService = weeklyReportDataAggregatorService;
        }

        public async Task<WeeklyReportResponseViewModel> FetchWeekly(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels)
        {
            var dates = GetAllDayForTheWeek(date);

            var weekOfYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var data = new List<WeeklyReportDataResponseViewModel>();

            foreach (var item in dates)
            {
                var model = await _weeklyReportDataAggregatorService.AggregateForDay(item, tramModels);

                if (model == default) continue;

                data.Add(model);
            }

            return new WeeklyReportResponseViewModel { WeekOfYear = weekOfYear, Models = data };
        }

        private IEnumerable<DateTime> GetAllDayForTheWeek(DateTime date)
        {
            var currentDayOfWeek = (int)date.DayOfWeek;
            var sunday = date.AddDays(-currentDayOfWeek);
            var monday = sunday.AddDays(1);

            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }

            return Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
        }
    }
}
