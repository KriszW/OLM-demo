using ClosedXML.Excel;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using OLM.Services.DailyReport.API.ViewModels;
using System.Collections.Generic;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager.Common
{
    public class TargetWorkBookWriter : ITargetWorkBookWriter
    {
        public IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<TargetResponseViewModel> models)
        {
            var rowIndex = 1;

            sheet.Cell(rowIndex, 1).Value = "Dimenzió";
            sheet.Cell(rowIndex, 2).Value = "Target érték";
            sheet.Cell(rowIndex, 3).Value = "Keresztmetszet";
            rowIndex++;

            foreach (var item in models)
            {
                sheet.Cell(rowIndex, 1).Value = item.Dimension;
                sheet.Cell(rowIndex, 2).Value = item.Target;
                sheet.Cell(rowIndex, 3).Value = item.Intersection;

                rowIndex++;
            }

            return sheet;
        }
    }
}
