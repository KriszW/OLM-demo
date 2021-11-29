using OLM.Services.DailyReport.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface IDestinationRepository
    {
        Task<DateTime> GetLatestDate();

        Task UploadNewModels(IEnumerable<DailyReportModel> models);
    }
}
