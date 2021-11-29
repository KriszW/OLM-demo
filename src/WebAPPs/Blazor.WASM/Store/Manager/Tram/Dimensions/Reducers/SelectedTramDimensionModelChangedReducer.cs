using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Reducers
{
    public class SelectedTramDimensionModelChangedReducer : Reducer<TramDimensionsState, SelectedTramDimensionModelChangedAction>
    {
        public override TramDimensionsState Reduce(TramDimensionsState state, SelectedTramDimensionModelChangedAction action)
            => new TramDimensionsState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     action.NewSelectedModel,
                                     default,
                                     default);
    }
}
