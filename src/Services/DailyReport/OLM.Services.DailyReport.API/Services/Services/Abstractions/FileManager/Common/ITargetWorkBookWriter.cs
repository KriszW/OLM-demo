using ClosedXML.Excel;
using OLM.Services.DailyReport.API.ViewModels;
using System.Collections.Generic;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common
{
    public interface ITargetWorkBookWriter
    {
        IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<TargetResponseViewModel> models);
    }
}
