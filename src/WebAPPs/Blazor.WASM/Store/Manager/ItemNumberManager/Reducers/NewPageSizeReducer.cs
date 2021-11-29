using Fluxor;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Reducers
{
    public class NewPageSizeReducer : Reducer<CatBulbItemNumberManagerState, PageSizeChangedAction>
    {
        public override CatBulbItemNumberManagerState Reduce(CatBulbItemNumberManagerState state, PageSizeChangedAction action)
            => new CatBulbItemNumberManagerState(state.PageIndex,
                                                 action.NewPageSize,
                                                 state.IsLoading,
                                                 state.CategorySearchQuery,
                                                 state.Errors,
                                                 state.Data,
                                                 default,
                                                 default,
                                                 default);
    }
}
