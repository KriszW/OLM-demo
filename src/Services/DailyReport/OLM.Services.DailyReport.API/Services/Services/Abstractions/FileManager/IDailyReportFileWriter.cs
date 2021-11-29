using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager
{
    public interface IDailyReportFileWriter
    {
        Task<Stream> CreateDimensionForDailyFile(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels);
        Task<Stream> CreateDimensionForWeekFile(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels);
    }
}
