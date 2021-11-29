using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Reducers
{
    public class FetchWasteTargetsFailedReducer : Reducer<WasteTargetManagerState, FetchWasteTargetsFailedAction>
    {
        public override WasteTargetManagerState Reduce(WasteTargetManagerState state, FetchWasteTargetsFailedAction action)
            => new WasteTargetManagerState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     action.Errors,
                                     default,
                                     default,
                                     default,
                                     default);
    }
}
