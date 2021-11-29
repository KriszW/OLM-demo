using ClosedXML.Excel;
using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager.Common;
using System.Collections.Generic;
using System.Linq;

namespace OLM.Services.DailyReport.API.Services.Services.Implementations.FileManager.Common
{
    public class DailyReportWorkBookWriter : IDailyReportWorkBookWriter
    {
        public IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<IGrouping<string, DailyReportDataModel>> data)
        {
            var rowIndex = 1;

            sheet.Cell(rowIndex, 1).Value = "ID";
            sheet.Cell(rowIndex, 2).Value = "Dimenzió";
            sheet.Cell(rowIndex, 3).Value = "Dátum";
            sheet.Cell(rowIndex, 4).Value = "Teljes hossz";
            sheet.Cell(rowIndex, 5).Value = "Hossztoldó hossza";
            sheet.Cell(rowIndex, 6).Value = "Veszteség hossza";
            rowIndex++;

            foreach (var item in data)
            {
                foreach (var reportData in item)
                {
                    sheet.Cell(rowIndex, 1).Value = reportData.ID.GetValueOrDefault();
                    sheet.Cell(rowIndex, 2).Value = reportData.Dimension;
                    sheet.Cell(rowIndex, 3).Value = reportData.Date;
                    sheet.Cell(rowIndex, 4).Value = reportData.Length;
                    sheet.Cell(rowIndex, 5).Value = reportData.LengthOfFS;
                    sheet.Cell(rowIndex, 6).Value = reportData.LengthOfWaste;

                    rowIndex++;
                }
            }

            return sheet;
        }

        public IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<DailyReportDataModel> data)
        {
            var rowIndex = 1;

            sheet.Cell(rowIndex, 1).Value = "ID";
            sheet.Cell(rowIndex, 2).Value = "Dimenzió";
            sheet.Cell(rowIndex, 3).Value = "Dátum";
            sheet.Cell(rowIndex, 4).Value = "Teljes hossz";
            sheet.Cell(rowIndex, 5).Value = "Hossztoldó hossza";
            sheet.Cell(rowIndex, 6).Value = "Veszteség hossza";
            rowIndex++;

            foreach (var item in data)
            {
                sheet.Cell(rowIndex, 1).Value = item.ID.GetValueOrDefault();
                sheet.Cell(rowIndex, 2).Value = item.Dimension;
                sheet.Cell(rowIndex, 3).Value = item.Date;
                sheet.Cell(rowIndex, 4).Value = item.Length;
                sheet.Cell(rowIndex, 5).Value = item.LengthOfFS;
                sheet.Cell(rowIndex, 6).Value = item.LengthOfWaste;

                rowIndex++;
            }

            return sheet;
        }
    }
}
