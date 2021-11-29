using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Reducers
{
    public class FetchWasteTargetsSuccessReducer : Reducer<WasteTargetManagerState, FetchWasteTargetsSuccessAction>
    {
        public override WasteTargetManagerState Reduce(WasteTargetManagerState state, FetchWasteTargetsSuccessAction action)
            => new WasteTargetManagerState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     default,
                                     action.Model,
                                     default,
                                     default,
                                     default);
    }
}
