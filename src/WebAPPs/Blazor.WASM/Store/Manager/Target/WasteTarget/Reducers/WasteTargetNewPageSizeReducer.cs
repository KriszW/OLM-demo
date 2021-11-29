using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Reducers
{
    public class WasteTargetNewPageSizeReducer : Reducer<WasteTargetManagerState, WasteTargetPageSizeChangedAction>
    {
        public override WasteTargetManagerState Reduce(WasteTargetManagerState state, WasteTargetPageSizeChangedAction action)
            => new WasteTargetManagerState(state.PageIndex,
                                     action.NewPageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
