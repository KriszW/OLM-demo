using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Reducers
{
    public class SetDeletingModelReducer : Reducer<TCOSettingsManagerState, TCOSettingsForDeletingAction>
    {
        public override TCOSettingsManagerState Reduce(TCOSettingsManagerState state, TCOSettingsForDeletingAction action)
            => new TCOSettingsManagerState(state.PageIndex,
                                           state.PageSize,
                                           state.IsLoading,
                                           state.Errors,
                                           state.Data,
                                           state.SelectedModel,
                                           state.ModelForEdit,
                                           action.Model);
    }
}
