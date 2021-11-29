using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Reducers
{
    public class ChangeTramDimensionModelReducer : Reducer<TramDimensionsState, ChangeTramDimensionModelAction>
    {
        public override TramDimensionsState Reduce(TramDimensionsState state, ChangeTramDimensionModelAction action)
            => new TramDimensionsState(state.PageIndex,
                                     state.PageSize,
                                     state.IsLoading,
                                     state.Errors,
                                     state.Data,
                                     state.SelectedModel,
                                     action.NewModelForEdit,
                                     default);
    }
}
