using Fluxor;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Reducers
{
    public class FetchModelSucceededReducer : Reducer<CatBulbItemNumberManagerState, FetchCatBulbItemNumberModelSuccessAction>
    {
        public override CatBulbItemNumberManagerState Reduce(CatBulbItemNumberManagerState state, FetchCatBulbItemNumberModelSuccessAction action)
            => new CatBulbItemNumberManagerState(state.PageIndex,
                                                 state.PageSize,
                                                 false,
                                                 state.CategorySearchQuery,
                                                 default,
                                                 action.Data,
                                                 default,
                                                 default,
                                                 default);
    }
}
