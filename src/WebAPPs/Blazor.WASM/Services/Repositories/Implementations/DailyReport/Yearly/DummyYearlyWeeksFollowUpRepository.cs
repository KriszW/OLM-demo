using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Yearly;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Yearly
{
    public class DummyYearlyWeeksFollowUpRepository : IYearlyWeeksFollowUpRepository
    {
        public Task<APIResponse<WeeksReportResponseViewModel>> Fetch(DateTime start, DateTime end)
            => Task.FromResult(new APIResponse<WeeksReportResponseViewModel> {
                
                Model = new WeeksReportResponseViewModel(start, end, new List<WeeksReportDataResponseViewModel> 
                {
                    new WeeksReportDataResponseViewModel(DateTime.Now.AddDays(-1),DateTime.Now.AddDays(1), 23, DateTime.Now.Year)
                    {
                        TotalSawPercent = 0.1384,
                        TotalFSPercent = 0.0038,
                        TotalTramPercent = 0.0064,
                        TotalLamellaPercent = 0.0033,
                        TotalExcludedPlankPercent = 0.0,
                        TotalWastePercent = 0.1530,
                        TotalTargetPercent = 0.1780,
                    },
                    new WeeksReportDataResponseViewModel(DateTime.Now.AddDays(-13),DateTime.Now.AddDays(-6), 22, DateTime.Now.Year)
                    {
                        TotalSawPercent = 0.1426,
                        TotalFSPercent = 0.0055,
                        TotalTramPercent = 0.0022,
                        TotalLamellaPercent = 0.0042,
                        TotalExcludedPlankPercent = 0.0,
                        TotalWastePercent = 0.1561,
                        TotalTargetPercent = 0.1716,
                    },
                    new WeeksReportDataResponseViewModel(DateTime.Now.AddDays(-21),DateTime.Now.AddDays(-14), 21, DateTime.Now.Year)
                    {
                        TotalSawPercent = 0.1878,
                        TotalFSPercent = 0.0061,
                        TotalTramPercent = 0.0043,
                        TotalLamellaPercent = 0.0028,
                        TotalExcludedPlankPercent = 0.0,
                        TotalWastePercent = 0.2028,
                        TotalTargetPercent = 0.1642,
                    },
                })

            });
    }
}
