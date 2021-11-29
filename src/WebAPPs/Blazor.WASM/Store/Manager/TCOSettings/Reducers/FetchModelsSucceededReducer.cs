using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Reducers
{
    public class FetchModelsSucceededReducer : Reducer<TCOSettingsManagerState, FetchSettingsModelSuccessAction>
    {
        public override TCOSettingsManagerState Reduce(TCOSettingsManagerState state, FetchSettingsModelSuccessAction action)
            => new TCOSettingsManagerState(state.PageIndex,
                                           state.PageSize,
                                           false,
                                           default,
                                           action.Data,
                                           default,
                                           default,
                                           default);
    }
}
