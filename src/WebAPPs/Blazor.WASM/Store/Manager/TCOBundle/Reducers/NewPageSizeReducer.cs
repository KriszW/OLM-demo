using Fluxor;
using OLM.Blazor.WASM.Store.Manager.BundlePrices;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Reducers
{
    public class NewPageSizeReducer : Reducer<TCOBundleState, PageSizeChangedAction>
    {
        public override TCOBundleState Reduce(TCOBundleState state, PageSizeChangedAction action)
            => new TCOBundleState(state.PageIndex,
                                  action.NewPageSize,
                                  false,
                                  state.From,
                                  state.To,
                                  default,
                                  default);
    }
}
