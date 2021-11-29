using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Reducers
{
    public class EditTCOSettingsModelReducer : Reducer<TCOSettingsManagerState, EditTCOSettingsModelAction>
    {
        public override TCOSettingsManagerState Reduce(TCOSettingsManagerState state, EditTCOSettingsModelAction action)
            => new TCOSettingsManagerState(state.PageIndex,
                                           state.PageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           state.SelectedModel,
                                           action.ModelForEdit,
                                           default);
    }
}
