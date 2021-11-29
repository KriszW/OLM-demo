using ClosedXML.Excel;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System.Collections.Generic;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager.Common
{
    public class TramModelWorkBookWriter : ITramModelWorkBookWriter
    {
        public IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<DailyReportRequestTramViewModel> models)
        {
            var rowIndex = 1;

            sheet.Cell(rowIndex, 1).Value = "Dimenzió";
            sheet.Cell(rowIndex, 2).Value = "Dátum";
            sheet.Cell(rowIndex, 3).Value = "Csillék száma";
            sheet.Cell(rowIndex, 4).Value = "Lamellák száma";
            rowIndex++;

            foreach (var item in models)
            {
                sheet.Cell(rowIndex, 1).Value = item.Dimension;
                sheet.Cell(rowIndex, 2).Value = item.Date;
                sheet.Cell(rowIndex, 3).Value = item.NumberOfTram;
                sheet.Cell(rowIndex, 4).Value = item.NumberOfLammela;

                rowIndex++;
            }

            return sheet;
        }
    }
}
