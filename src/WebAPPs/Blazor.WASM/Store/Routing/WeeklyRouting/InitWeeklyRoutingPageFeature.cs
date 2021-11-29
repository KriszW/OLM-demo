using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting
{
    public class InitWeeklyRoutingPageFeature : Feature<WeeklyRoutingPageState>
    {
        public override string GetName() => "InitWeeklyRoutingPageFeature";

        protected override WeeklyRoutingPageState GetInitialState() => new WeeklyRoutingPageState(default);
    }
}
