using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
 
namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Reducers
{
    public class FetchRoutingManagersSuccessReducer : Reducer<RoutingManagerPageState, FetchRoutingManagersSuccessAction>
    {
        public override RoutingManagerPageState Reduce(RoutingManagerPageState state, FetchRoutingManagersSuccessAction action)
            => new RoutingManagerPageState(state.PageIndex,
                                           state.PageSize,
                                           false,
                                           default,
                                           action.Model,
                                           default,
                                           default,
                                           default);
    }
}
