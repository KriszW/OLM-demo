using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions
{
    public interface IBundleMachineAggregator
    {
        Task<APIResponse<MachineViewModel>> GetDataForMachine(string machineID);
        Task<APIResponse<SummarizedMachineViewModel>> GetDataForMachines();
    }
}
