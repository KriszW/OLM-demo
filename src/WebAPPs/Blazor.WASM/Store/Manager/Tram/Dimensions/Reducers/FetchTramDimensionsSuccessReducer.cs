using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Reducers
{
    public class FetchTramDimensionsSuccessReducer : Reducer<TramDimensionsState, FetchTramDimensionsSuccessAction>
    {
        public override TramDimensionsState Reduce(TramDimensionsState state, FetchTramDimensionsSuccessAction action)
            => new TramDimensionsState(state.PageIndex,
                                     state.PageSize,
                                     false,
                                     default,
                                     action.Model,
                                     default,
                                     default,
                                     default);
    }
}
