using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager
{
    public class InitRouteManagerPageFeature : Feature<RoutingManagerPageState>
    {
        public override string GetName() => "InitRoutingManagerCRUDPage";

        protected override RoutingManagerPageState GetInitialState() => new RoutingManagerPageState(0,25);
    }
}
