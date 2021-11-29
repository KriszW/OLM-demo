using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Reducers
{
    public class NewPageSizeReducer : Reducer<TCOSettingsManagerState, PageSizeChangedAction>
    {
        public override TCOSettingsManagerState Reduce(TCOSettingsManagerState state, PageSizeChangedAction action)
            => new TCOSettingsManagerState(state.PageIndex,
                                           action.NewPageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           default,
                                           default,
                                           default);
    }
}
