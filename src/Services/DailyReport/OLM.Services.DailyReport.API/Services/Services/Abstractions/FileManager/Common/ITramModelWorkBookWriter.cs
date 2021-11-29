using ClosedXML.Excel;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System.Collections.Generic;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common
{
    public interface ITramModelWorkBookWriter
    {
        IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<DailyReportRequestTramViewModel> models);
    }
}
