using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Reducers
{
    public class FetchRoutingPausesManagersSuccessReducer : Reducer<RoutingPausesPageState, FetchRoutingPausesManagersSuccessAction>
    {
        public override RoutingPausesPageState Reduce(RoutingPausesPageState state, FetchRoutingPausesManagersSuccessAction action)
            => new RoutingPausesPageState(state.PageIndex,
                                          state.PageSize,
                                          false,
                                          default,
                                          action.Model);
    }
}
