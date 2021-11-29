using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Abstractions.Machine
{
    public interface IMachineBundlesRepository
    {
        Task<SummarizedBundlesForMachineDataViewModel> GetDailySummarizedData(string machineName);
        Task<SummarizedBundlesForMachineDataViewModel> GetWeeklySummarizedData(string machineName);
    }
}
