using Fluxor;
using OLM.Blazor.WASM.Store.Machines.Machine.Actions;
using OLM.Blazor.WASM.Store.Machines.MachinesSummarized;

namespace OLM.Blazor.WASM.Store.Machines.Machine.Reducers
{
    public class FetchMachineDataWithErrorReducer : Reducer<MachineState, FetchMachineDataWithErrorAction>
    {
        public override MachineState Reduce(MachineState state, FetchMachineDataWithErrorAction action) 
            => new MachineState(false, action.Response, state.MachineID);
    }
}
