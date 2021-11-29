using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Abstractions.Machine
{
    public interface IMachineSumRepository
    {
        Task<SummarizedDataForMachinesViewModel> GetAllDailyBundles();
        Task<SummarizedDataForMachinesViewModel> GetAllWeeklyBundles();
    }
}
