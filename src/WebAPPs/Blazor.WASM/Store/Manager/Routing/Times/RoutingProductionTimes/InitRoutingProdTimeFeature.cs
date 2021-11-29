using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes
{
    public class InitRoutingProdTimeFeature : Feature<RoutingProductionTimePageState>
    {
        public override string GetName() => "InitRoutingProdTimeManager";

        protected override RoutingProductionTimePageState GetInitialState() => new RoutingProductionTimePageState(0, 25);
    }
}
