using Fluxor;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Reducers
{
    public class EditCatBulbItemNumberModelChangedReducer : Reducer<CatBulbItemNumberManagerState, EditCatBulbItemNumberModelAction>
    {
        public override CatBulbItemNumberManagerState Reduce(CatBulbItemNumberManagerState state, EditCatBulbItemNumberModelAction action)
            => new CatBulbItemNumberManagerState(state.PageIndex,
                                                 state.PageSize,
                                                 state.IsLoading,
                                                 state.CategorySearchQuery,
                                                 state.Errors,
                                                 state.Data,
                                                 state.SelectedModel,
                                                 action.Model,
                                                 default);
    }
}
