using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions
{
    public class RoutingProductionTimeManagerPageIndexChangedAction
    {
        public RoutingProductionTimeManagerPageIndexChangedAction(int newPageIndex)
        {
            NewPageIndex = newPageIndex;
        }

        public int NewPageIndex { get; private set; }
    }
}
