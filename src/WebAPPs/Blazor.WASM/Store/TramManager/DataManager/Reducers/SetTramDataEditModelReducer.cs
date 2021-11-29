using Fluxor;
using OLM.Blazor.WASM.Store.TramManager.DataManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Reducers
{
    public class SetTramDataEditModelReducer : Reducer<TramDataManagerState, SetTramDataEditModelAction>
    {
        public override TramDataManagerState Reduce(TramDataManagerState state, SetTramDataEditModelAction action)
            => new TramDataManagerState(state.MachineID, state.Dimensions, state.Errors, action.NewModel);
    }
}
