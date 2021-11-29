using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Machine
{
    public interface IMachineRepository
    {
        Task<APIResponse<MachineViewModel>> FetchMachineData(string machineName);
        Task<APIResponse<SummarizedMachineViewModel>> FetchSummarizedData();
    }
}
