using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Reducers
{
    public class FetchTCOBundleSuccessReducer : Reducer<TCOBundleState, FetchTCOBundleSuccessAction>
    {
        public override TCOBundleState Reduce(TCOBundleState state, FetchTCOBundleSuccessAction action)
            => new TCOBundleState(state.PageIndex,
                                  state.PageSize,
                                  false,
                                  state.From,
                                  state.To,
                                  default,
                                  action.Model);
    }
}
