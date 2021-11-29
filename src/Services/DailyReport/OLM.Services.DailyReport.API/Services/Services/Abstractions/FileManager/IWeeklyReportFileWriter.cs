using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager
{
    public interface IWeeklyReportFileWriter
    {
        Task<Stream> CreateWeeklyFile(DateTime date, IEnumerable<DailyReportRequestTramViewModel> tramModels);
    }
}
