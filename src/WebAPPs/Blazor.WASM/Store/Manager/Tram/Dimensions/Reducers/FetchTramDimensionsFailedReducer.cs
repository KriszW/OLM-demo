using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Reducers
{
    public class FetchTramDimensionsFailedReducer : Reducer<TramDimensionsState, FetchTramDimensionsFailedAction>
    {
        public override TramDimensionsState Reduce(TramDimensionsState state, FetchTramDimensionsFailedAction action)
            => new TramDimensionsState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     action.Errors,
                                     default,
                                     default,
                                     default,
                                     default);
    }
}
