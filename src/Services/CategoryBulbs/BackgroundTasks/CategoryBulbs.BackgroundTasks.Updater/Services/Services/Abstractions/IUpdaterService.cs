using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Services.Abstractions
{
    public interface IUpdaterService
    {
        DateTime LatestFinishedDate { get; set; }

        Task UpdateFinishedDate();

        Task Update();
    }
}
