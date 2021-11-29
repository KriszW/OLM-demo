using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Reducers
{
    public class TramDimensionNewPageIndexReducer : Reducer<TramDimensionsState, TramDimensionPageIndexChangedAction>
    {
        public override TramDimensionsState Reduce(TramDimensionsState state, TramDimensionPageIndexChangedAction action)
            => new TramDimensionsState(action.NewPageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
