using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Abstractions
{
    public interface ITCOUpdaterService
    {
        DateTime LatestFinishedDate { get; set; }

        Task UpdateFinishedDate();

        Task Update();
    }
}
