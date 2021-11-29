using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;

namespace OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Actions
{
    public class FetchSummarizedMachineDataWithErrorAction
    {
        public FetchSummarizedMachineDataWithErrorAction(APIResponse<SummarizedMachineViewModel> response)
        {
            Response = response;
        }

        public APIResponse<SummarizedMachineViewModel> Response { get; set; }
    }
}
