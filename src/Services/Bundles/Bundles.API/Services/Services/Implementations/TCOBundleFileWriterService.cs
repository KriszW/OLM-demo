using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Services.Implementations
{
    public class TCOBundleFileWriterService : ITCOBundleFileWriterService
    {
        public Task<Stream> WriteToFile(IEnumerable<TCOBundleAPIResponseViewModel> data)
        {
            using var workBook = new XLWorkbook();

            Write(workBook.AddWorksheet("TCO Data"), data);

            return Task.FromResult(SaveWorkBookToStream(workBook));
        }

        private void AddHeader(IXLWorksheet sheet)
        {
            var rowIndex = 1;

            var headers = new string[]
            {
                "BundleID",
                "Beszállító",
                "Fűrészüzem",
                "Teljes hossz",
                "Jó hossza",
                "Hossztoldó hossza",
                "Jó ráta",
                "Standard TCO",
                "Aktuális TCO",
                "Bejezeés ideje",
                "Dimenzió",
                "Cikkszám",
            };

            for (int i = 0; i < headers.Length; i++)
            {
                sheet.Cell(rowIndex, i+1).Value = headers[i];
            }
        }

        private IXLWorksheet Write(IXLWorksheet sheet, IEnumerable<TCOBundleAPIResponseViewModel> data)
        {
            AddHeader(sheet);

            var rowIndex = 2;

            foreach (var item in data)
            {
                sheet.Cell(rowIndex, 1).Value = item.BundleID;
                sheet.Cell(rowIndex, 2).Value = item.Vendor;
                sheet.Cell(rowIndex, 3).Value = item.Sawmill;
                sheet.Cell(rowIndex, 4).Value = item.Input;
                sheet.Cell(rowIndex, 5).Value = item.Good;
                sheet.Cell(rowIndex, 6).Value = item.FS;
                sheet.Cell(rowIndex, 7).Value = item.GoodRate;
                sheet.Cell(rowIndex, 8).Value = item.StandardTCO;
                sheet.Cell(rowIndex, 9).Value = item.ActualTCO;
                sheet.Cell(rowIndex, 10).Value = item.FinishedDate;
                sheet.Cell(rowIndex, 11).Value = item.Dimension;
                sheet.Cell(rowIndex, 12).Value = item.MaterialNumber;

                rowIndex++;
            }

            return sheet;
        }

        private Stream SaveWorkBookToStream(XLWorkbook workBook)
        {
            var output = new MemoryStream();
            workBook.SaveAs(output);

            return output;
        }
    }
}
