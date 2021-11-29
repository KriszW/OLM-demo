using Fluxor;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Reducers
{
    public class SelectedCatItemNumberChangedReducer : Reducer<CatBulbItemNumberManagerState, SelectedCatItemNumberModelChangedAction>
    {
        public override CatBulbItemNumberManagerState Reduce(CatBulbItemNumberManagerState state, SelectedCatItemNumberModelChangedAction action)
            => new CatBulbItemNumberManagerState(state.PageIndex,
                                                 state.PageSize,
                                                 state.IsLoading,
                                                 state.CategorySearchQuery,
                                                 state.Errors,
                                                 state.Data,
                                                 action.NewSelectedModel,
                                                 default,
                                                 default);
    }
}
