using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Extensions
{
    public static class DailyReportSummarizedForDimensionViewModelExtensions
    {
        public static DimensionReportSummarizedDataResponseViewModel Summarize(this DailyReportSummarizedForDimensionViewModel model, TargetResponseViewModel target)
        {
            return new DimensionReportSummarizedDataResponseViewModel
            {
                Dimension = target.Dimension,
                SawPercent = model.WasteM3 / model.TotalM3,
                FSPercent = model.FSM3 / model.TotalM3,
                LamellaPercent = model.LammelaM3 / model.TotalM3,
                TramPercent = model.TramM3 / model.TotalM3,
                ExcludedPlankPercent = 0.0,
                Target = target.Target,
            };
        }
    }
}
