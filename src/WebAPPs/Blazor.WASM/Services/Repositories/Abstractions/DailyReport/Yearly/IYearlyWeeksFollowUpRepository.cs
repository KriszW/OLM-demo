using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Yearly
{
    public interface IYearlyWeeksFollowUpRepository
    {
        Task<APIResponse<WeeksReportResponseViewModel>> Fetch(DateTime start, DateTime end);
    }
}
