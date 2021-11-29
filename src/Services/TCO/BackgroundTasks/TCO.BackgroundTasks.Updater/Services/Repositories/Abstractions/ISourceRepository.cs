using OLM.Services.TCO.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Abstractions
{
    public interface ISourceRepository
    {
        Task<DateTime> GetDatetimeForBundle(string bundleID);

        Task<IEnumerable<TCODataSourceModel>> GetModelsFrom(DateTime date);
    }
}
