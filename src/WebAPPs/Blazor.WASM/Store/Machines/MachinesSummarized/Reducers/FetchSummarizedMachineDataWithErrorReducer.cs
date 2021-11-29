using Fluxor;
using OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Actions;

namespace OLM.Blazor.WASM.Store.Machines.MachinesSummarized.Reducers
{
    public class FetchSummarizedMachineDataWithErrorReducer : Reducer<MachineSummerizedState, FetchSummarizedMachineDataWithErrorAction>
    {
        public override MachineSummerizedState Reduce(MachineSummerizedState state, FetchSummarizedMachineDataWithErrorAction action) => new MachineSummerizedState(action.Response);
    }
}
