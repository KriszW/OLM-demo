using Fluxor;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Reducers
{
    public class NewPageIndexReducer : Reducer<CatBulbItemNumberManagerState, PageIndexChangedAction>
    {
        public override CatBulbItemNumberManagerState Reduce(CatBulbItemNumberManagerState state, PageIndexChangedAction action)
        => new CatBulbItemNumberManagerState(action.NewPageIndex,
                                             state.PageSize,
                                             state.IsLoading,
                                             state.CategorySearchQuery,
                                             state.Errors,
                                             state.Data,
                                             default,
                                             default,
                                             default);
    }
}
