using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Dimension;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Dimension
{
    public class DummyDimensionFollowUpRepository : IDimensionFollowUpRepository
    {
        public Task<APIResponse<DimensionReportSummarizedResponseViewModel>> FetchDayReport(DateTime date)
            => Task.FromResult(new APIResponse<DimensionReportSummarizedResponseViewModel>(new DimensionReportSummarizedResponseViewModel 
            {
                Date = date,
                DimensionReportData = new List<DimensionReportSummarizedDataResponseViewModel> 
                {
                    new DimensionReportSummarizedDataResponseViewModel
                    {
                        Dimension = "25x75",
                        SawPercent = 0.1514,
                        FSPercent = 0.0068,
                        TramPercent = 0.0,
                        LamellaPercent = 0.0027,
                        Target = 0.1961,
                    },
                    new DimensionReportSummarizedDataResponseViewModel
                    {
                        Dimension = "19x62,5",
                        SawPercent = 0.1318,
                        FSPercent = 0.0005,
                        TramPercent = 0.0127,
                        LamellaPercent = 0.0,
                        Target = 0.1833,
                    },
                    new DimensionReportSummarizedDataResponseViewModel
                    {
                        Dimension = "25x62,5",
                        SawPercent = 0.0953,
                        FSPercent = 0.0028,
                        LamellaPercent = 0.0110,
                        TramPercent = 0.0116,
                        Target = 0.1521,
                    },
                    new DimensionReportSummarizedDataResponseViewModel
                    {
                        Dimension = "38x175",
                        SawPercent = 0.2791,
                        FSPercent = 0.0060,
                        LamellaPercent = 0.0,
                        TramPercent = 0.0,
                        Target = 0.2214,
                    },
                    new DimensionReportSummarizedDataResponseViewModel
                    {
                        Dimension = "25x100",
                        SawPercent = 0.1491,
                        FSPercent = 0.0054,
                        LamellaPercent = 0.0054,
                        TramPercent = 0.0060,
                        Target = 0.1915,
                    },
                }

            }));

        public Task<APIResponse<DimensionReportSummarizedResponseViewModel>> FetchWeeklyReport(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
