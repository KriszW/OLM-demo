using Fluxor;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Reducers
{
    public class ToChangedReducer : Reducer<TCOBundleState, ToChangedAction>
    {
        public override TCOBundleState Reduce(TCOBundleState state, ToChangedAction action)
            => new TCOBundleState(state.PageIndex,
                                  state.PageSize,
                                  false,
                                  state.From,
                                  action.To,
                                  default,
                                  default);
    }
}
