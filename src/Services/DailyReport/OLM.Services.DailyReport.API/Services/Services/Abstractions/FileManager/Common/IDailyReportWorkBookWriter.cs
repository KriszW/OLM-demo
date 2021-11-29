using ClosedXML.Excel;
using OLM.Services.DailyReport.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common
{
    public interface IDailyReportWorkBookWriter
    {
        IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<IGrouping<string, DailyReportDataModel>> data);
        IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<DailyReportDataModel> data);
    }
}
