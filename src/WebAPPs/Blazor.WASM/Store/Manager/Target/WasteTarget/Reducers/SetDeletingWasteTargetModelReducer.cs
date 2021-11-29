using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Reducers
{
    public class SetDeletingWasteTargetModelReducer : Reducer<WasteTargetManagerState, WasteTargetModelForDeletingSetAction>
    {
        public override WasteTargetManagerState Reduce(WasteTargetManagerState state, WasteTargetModelForDeletingSetAction action)
            => new WasteTargetManagerState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     state.ModelForEdit,
                                     action.Model);
    }
}
