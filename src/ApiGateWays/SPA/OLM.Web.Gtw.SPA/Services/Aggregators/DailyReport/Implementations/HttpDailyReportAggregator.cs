using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Implementations
{
    public class HttpDailyReportAggregator : IDailyReportAggregator
    {
        private readonly IDailyReportService _dailyReportService;
        private readonly IWeeklyReportService _weeklyReportService;
        private readonly IWeeksReportService _weeksReportService;
        private readonly ITramService _tramService;

        public HttpDailyReportAggregator(IDailyReportService dailyReportService,
                                         IWeeklyReportService weeklyReportService,
                                         IWeeksReportService weeksReportService,
                                         ITramService tramService)
        {
            _dailyReportService = dailyReportService;
            _weeklyReportService = weeklyReportService;
            _weeksReportService = weeksReportService;
            _tramService = tramService;
        }

        public async Task<DimensionReportSummarizedResponseViewModel> FetchDimensionForDay(DateTime date)
        {
            var start = date.Date;
            var end = date.Date.Add(new TimeSpan(23,59,59));

            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = start, End = end });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _dailyReportService.FechDimensionForDay(date, tramModels);
        }

        public async Task<DimensionReportSummarizedResponseViewModel> FetchDimensionForWeek(DateTime date)
        {
            var start = date.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = start, End = end });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _dailyReportService.FechDimensionForWeek(date, tramModels);
        }

        public async Task<WeeklyReportResponseViewModel> FetchWeeklyReport(DateTime date)
        {
            var start = date.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = start, End = end });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _weeklyReportService.FetchWeeklyReport(date, tramModels);
        }

        public async Task<WeeksReportResponseViewModel> FetchYearlyWeeks(WeeksRequestViewModel model)
        {
            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = model.Start, End = model.End });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _weeksReportService.FetchYearlyWeeks(model, tramModels);
        }
    }
}
