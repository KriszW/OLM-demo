using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Reducers
{
    public class NewPageIndexReducer : Reducer<TCOBundleState, PageIndexChangedAction>
    {
        public override TCOBundleState Reduce(TCOBundleState state, PageIndexChangedAction action)
            => new TCOBundleState(action.NewPageIndex,
                                  state.PageSize,
                                  false,
                                  state.From,
                                  state.To,
                                  default,
                                  default);
    }
}
