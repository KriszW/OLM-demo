using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;

namespace OLM.Blazor.WASM.Store.Machines.Machine
{
    public class MachineState
    {
        public MachineState() : this(true, default, default) { }

        public MachineState(APIResponse<MachineViewModel> response) : this(false, response, default) { }

        public MachineState(bool isLoading, APIResponse<MachineViewModel> response, string machineID)
        {
            IsLoading = isLoading;
            Response = response;
            MachineID = machineID;
        }

        public bool IsLoading { get; private set; }

        public APIResponse<MachineViewModel> Response { get; set; }

        public string MachineID { get; set; }
    }
}
