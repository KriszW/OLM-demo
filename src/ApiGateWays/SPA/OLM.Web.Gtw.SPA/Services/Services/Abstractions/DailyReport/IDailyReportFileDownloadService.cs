using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport
{
    public interface IDailyReportFileDownloadService
    {
        Task<byte[]> DownloadDimDaily(DateTime date, IEnumerable<TramResponseViewModel> models);
        Task<byte[]> DownloadDimWeekly(DateTime date, IEnumerable<TramResponseViewModel> models);
        Task<byte[]> DownloadWeekly(DateTime date, IEnumerable<TramResponseViewModel> models);
        Task<byte[]> DownloadWeeks(WeeksRequestViewModel model, IEnumerable<TramResponseViewModel> models);
    }
}
