using Fluxor;
using OLM.Blazor.WASM.Store.TramManager.DataManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Reducers
{
    public class MachineIDReducer : Reducer<TramDataManagerState, SetMachineIDAction>
    {
        public override TramDataManagerState Reduce(TramDataManagerState state, SetMachineIDAction action)
            => new TramDataManagerState(action.MachineID);
    }
}
