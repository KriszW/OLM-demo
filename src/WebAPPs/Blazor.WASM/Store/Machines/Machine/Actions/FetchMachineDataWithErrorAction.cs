using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;

namespace OLM.Blazor.WASM.Store.Machines.Machine.Actions
{
    public class FetchMachineDataWithErrorAction
    {
        public FetchMachineDataWithErrorAction(APIResponse<MachineViewModel> response)
        {
            Response = response;
        }

        public APIResponse<MachineViewModel> Response { get; set; }

    }
}
