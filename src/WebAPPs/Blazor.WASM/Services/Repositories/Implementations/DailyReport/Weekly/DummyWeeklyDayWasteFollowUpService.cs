using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Weekly;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Weekly
{
    public class DummyWeeklyDayWasteFollowUpService : IWeeklyDayFollowUpRepository
    {
        public Task<APIResponse<WeeklyReportResponseViewModel>> FetchDayReport(DateTime date)
            => Task.FromResult(new APIResponse<WeeklyReportResponseViewModel>(new WeeklyReportResponseViewModel 
            { 
                WeekOfYear = 20,
                Models = new List<WeeklyReportDataResponseViewModel>
                {
                    new WeeklyReportDataResponseViewModel
                    {
                        Date = new DateTime(2020,6,22),
                        TotalSawPercent = 0.1384,
                        TotalFSPercent = 0.0038,
                        TotalTramPercent = 0.0064,
                        TotalLamellaPercent = 0.0033,
                        TotalExcludedPlankPercent = 0.0,
                        TotalWastePercent = 0.1530,
                        TotalTargetPercent = 0.1780,
                    },
                    new WeeklyReportDataResponseViewModel
                    {
                        Date = new DateTime(2020,6,23),
                        TotalSawPercent = 0.1426,
                        TotalFSPercent = 0.0055,
                        TotalTramPercent = 0.0022,
                        TotalLamellaPercent = 0.0042,
                        TotalExcludedPlankPercent = 0.0,
                        TotalWastePercent = 0.1561,
                        TotalTargetPercent = 0.1716,
                    },
                    new WeeklyReportDataResponseViewModel
                    {
                        Date = new DateTime(2020,6,24),
                        TotalSawPercent = 0.1878,
                        TotalFSPercent = 0.0061,
                        TotalTramPercent = 0.0043,
                        TotalLamellaPercent = 0.0028,
                        TotalExcludedPlankPercent = 0.0,
                        TotalWastePercent = 0.2028,
                        TotalTargetPercent = 0.1642,
                    },
                }
            
            }));
    }
}
