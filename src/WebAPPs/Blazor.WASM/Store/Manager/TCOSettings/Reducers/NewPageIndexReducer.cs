using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Reducers
{
    public class NewPageIndexReducer : Reducer<TCOSettingsManagerState, PageIndexChangedAction>
    {
        public override TCOSettingsManagerState Reduce(TCOSettingsManagerState state, PageIndexChangedAction action)
            => new TCOSettingsManagerState(action.NewPageIndex,
                                           state.PageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           default,
                                           default,
                                           default);
    }
}
