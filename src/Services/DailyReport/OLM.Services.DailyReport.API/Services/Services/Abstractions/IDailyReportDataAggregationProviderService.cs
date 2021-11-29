using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions
{
    public interface IDailyReportDataAggregationProviderService
    {
        Task<DailyReportSummarizedForDimensionViewModel> AggregateForDimension(ModelAggregatorViewModel model);
    }
}
