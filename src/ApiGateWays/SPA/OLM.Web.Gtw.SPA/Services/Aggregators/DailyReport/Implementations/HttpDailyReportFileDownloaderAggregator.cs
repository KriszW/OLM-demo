using OLM.ApiGateWays.Web.Gtw.SPA.Extensions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Implementations
{
    public class HttpDailyReportFileDownloaderAggregator : IDailyReportFileDownloaderAggregator
    {
        private readonly ITramService _tramService;
        private readonly IDailyReportFileDownloadService _dailyReportFileDownloadService;

        public HttpDailyReportFileDownloaderAggregator(ITramService tramService,
                                                       IDailyReportFileDownloadService dailyReportFileDownloadService)
        {
            _tramService = tramService;
            _dailyReportFileDownloadService = dailyReportFileDownloadService;
        }

        public async Task<byte[]> DimensionDaily(DateTime date)
        {
            var start = date.Date;
            var end = date.Date.Add(new TimeSpan(23, 59, 59));

            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = start, End = end });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _dailyReportFileDownloadService.DownloadDimDaily(date, tramModels);
        }

        public async Task<byte[]> DimensionWeek(DateTime date)
        {
            var start = date.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = start, End = end });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _dailyReportFileDownloadService.DownloadDimWeekly(date, tramModels);
        }

        public async Task<byte[]> Weekly(DateTime date)
        {
            var start = date.GetDateOfTheStartOfWeek(DayOfWeek.Monday);
            var end = date.GetDateOfTheEndOfWeek(DayOfWeek.Sunday);

            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = start, End = end });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _dailyReportFileDownloadService.DownloadWeekly(date, tramModels);
        }

        public async Task<byte[]> Weeks(WeeksRequestViewModel model)
        {
            var tramModels = await _tramService.Fetch(new TramFetchRequestViewModel { Start = model.Start, End = model.End });

            if (tramModels == default || tramModels.Any() == false) return default;

            return await _dailyReportFileDownloadService.DownloadWeeks(model, tramModels);
        }
    }
}
