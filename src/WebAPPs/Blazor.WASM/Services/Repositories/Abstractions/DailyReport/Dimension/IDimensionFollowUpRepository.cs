using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Dimension
{
    public interface IDimensionFollowUpRepository
    {
        Task<APIResponse<DimensionReportSummarizedResponseViewModel>> FetchDayReport(DateTime date);
        Task<APIResponse<DimensionReportSummarizedResponseViewModel>> FetchWeeklyReport(DateTime date);
    }
}
