using ClosedXML.Excel;
using System.IO;

namespace OLM.Services.DailyReport.API.Extensions
{
    public static class WorkbookExtensions
    {
        public static Stream SaveWorkBookToStream(this XLWorkbook workBook)
        {
            var output = new MemoryStream();
            workBook.SaveAs(output);

            return output;
        }
    }
}
