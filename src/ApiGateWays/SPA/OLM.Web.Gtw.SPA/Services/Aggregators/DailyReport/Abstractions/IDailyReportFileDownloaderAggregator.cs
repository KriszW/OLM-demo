using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions
{
    public interface IDailyReportFileDownloaderAggregator
    {
        Task<byte[]> DimensionDaily(DateTime date);

        Task<byte[]> DimensionWeek(DateTime date);


        Task<byte[]> Weekly(DateTime date);


        Task<byte[]> Weeks(WeeksRequestViewModel model);
    }
}
