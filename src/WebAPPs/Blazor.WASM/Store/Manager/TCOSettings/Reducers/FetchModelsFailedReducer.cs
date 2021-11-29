using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Reducers
{
    public class FetchModelsFailedReducer : Reducer<TCOSettingsManagerState, FetchSettingsModelFailedAction>
    {
        public override TCOSettingsManagerState Reduce(TCOSettingsManagerState state, FetchSettingsModelFailedAction action)
            => new TCOSettingsManagerState(state.PageIndex,
                                           state.PageSize,
                                           false,
                                           action.Errors,
                                           default,
                                           default,
                                           default,
                                           default);
    }
}
