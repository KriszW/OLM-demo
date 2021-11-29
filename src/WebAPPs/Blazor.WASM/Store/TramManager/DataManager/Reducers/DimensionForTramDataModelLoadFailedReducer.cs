using Fluxor;
using OLM.Blazor.WASM.Store.TramManager.DataManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Reducers
{
    public class DimensionForTramDataModelLoadFailedReducer : Reducer<TramDataManagerState, DimensionForTramDataLoadFailAction>
    {
        public override TramDataManagerState Reduce(TramDataManagerState state, DimensionForTramDataLoadFailAction action)
            => new TramDataManagerState(state.MachineID, default, action.Errors, state.Model);
    }
}
