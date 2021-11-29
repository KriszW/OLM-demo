using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Reducers
{
    public class WasteTargetNewPageIndexReducer : Reducer<WasteTargetManagerState, WasteTargetPageIndexChangedAction>
    {
        public override WasteTargetManagerState Reduce(WasteTargetManagerState state, WasteTargetPageIndexChangedAction action)
            => new WasteTargetManagerState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
