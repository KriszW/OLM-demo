using Fluxor;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Reducers
{
    public class FetchModelFailedReducer : Reducer<CatBulbItemNumberManagerState, FetchCatBulbItemNumberModelFailedAction>
    {
        public override CatBulbItemNumberManagerState Reduce(CatBulbItemNumberManagerState state, FetchCatBulbItemNumberModelFailedAction action)
            => new CatBulbItemNumberManagerState(state.PageIndex,
                                                 state.PageSize,
                                                 false,
                                                 state.CategorySearchQuery,
                                                 action.Errors,
                                                 default,
                                                 default,
                                                 default,
                                                 default);
    }
}
