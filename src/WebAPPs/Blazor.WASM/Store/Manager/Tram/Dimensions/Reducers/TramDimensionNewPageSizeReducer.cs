using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Reducers
{
    public class TramDimensionNewPageSizeReducer : Reducer<TramDimensionsState, TramDimensionPageSizeChangedAction>
    {
        public override TramDimensionsState Reduce(TramDimensionsState state, TramDimensionPageSizeChangedAction action)
            => new TramDimensionsState(state.PageIndex,
                                     action.NewPageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     default,
                                     default,
                                     default);
    }
}
