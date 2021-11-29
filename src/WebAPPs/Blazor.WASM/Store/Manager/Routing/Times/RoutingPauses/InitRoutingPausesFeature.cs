using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses
{
    public class InitRoutingPausesFeature : Feature<RoutingPausesPageState>
    {
        public override string GetName() => "InitRoutingPausesManager";

        protected override RoutingPausesPageState GetInitialState() => new RoutingPausesPageState(0, 25);
    }
}
