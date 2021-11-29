using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting
{
    public class InitDailyRoutingPageFeature : Feature<DailyRoutingPageState>
    {
        public override string GetName() => "InitDailyRoutingPageFeature";

        protected override DailyRoutingPageState GetInitialState() => new DailyRoutingPageState(default);
    }
}
