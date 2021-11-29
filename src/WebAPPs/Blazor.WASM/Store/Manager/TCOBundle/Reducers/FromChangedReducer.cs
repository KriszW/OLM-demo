using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Reducers
{
    public class FromChangedReducer : Reducer<TCOBundleState, FromChangedAction>
    {
        public override TCOBundleState Reduce(TCOBundleState state, FromChangedAction action)
            => new TCOBundleState(state.PageIndex,
                                  state.PageSize,
                                  false,
                                  action.From,
                                  state.To,
                                  default,
                                  default);
    }
}
