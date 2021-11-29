using Fluxor;
using OLM.Blazor.WASM.Store.TramManager.DataManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Reducers
{
    public class DimensionForTramDataModelLoadSucceededReducer : Reducer<TramDataManagerState, DimensionForTramDataLoadSuccessAction>
    {
        public override TramDataManagerState Reduce(TramDataManagerState state, DimensionForTramDataLoadSuccessAction action)
            => new TramDataManagerState(state.MachineID, action.Dimensions, default, state.Model);
    }
}
