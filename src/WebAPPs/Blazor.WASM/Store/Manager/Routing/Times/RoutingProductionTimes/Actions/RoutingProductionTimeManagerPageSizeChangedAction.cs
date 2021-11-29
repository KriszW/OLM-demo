using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions
{
    public class RoutingProductionTimeManagerPageSizeChangedAction
    {
        public RoutingProductionTimeManagerPageSizeChangedAction(int newPageSize)
        {
            NewPageSize = newPageSize;
        }

        public int NewPageSize { get; private set; }
    }
}
