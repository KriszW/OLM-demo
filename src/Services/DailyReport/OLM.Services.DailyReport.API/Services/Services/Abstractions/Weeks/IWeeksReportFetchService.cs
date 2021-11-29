using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.Weeks
{
    public interface IWeeksReportFetchService
    {
        Task<WeeksReportResponseViewModel> Fetch(WeeksRequestViewModel model, IEnumerable<DailyReportRequestTramViewModel> tramModels);
    }
}
