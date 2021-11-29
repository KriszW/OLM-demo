using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Reducers
{
    public class FetchTCOBundleFailedReducer : Reducer<TCOBundleState, FetchTCOBundleFailedAction>
    {
        public override TCOBundleState Reduce(TCOBundleState state, FetchTCOBundleFailedAction action)
            => new TCOBundleState(state.PageIndex,
                                  state.PageSize,
                                  false,
                                  state.From,
                                  state.To,
                                  action.Errors,
                                  default);
    }
}
