using OLM.Services.DailyReport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Services.Repositories.Abstractions
{
    public interface ITargetRepository
    {
        Task<TargetResponseViewModel> GetForDimension(string dimension);

        Task<IEnumerable<TargetResponseViewModel>> GetForDimension(IEnumerable<string> dimensions);
    }
}
