using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions
{
    public class RoutingPausesManagerPageSizeChangedAction
    {
        public RoutingPausesManagerPageSizeChangedAction(int newPageSize)
        {
            NewPageSize = newPageSize;
        }

        public int NewPageSize { get; private set; }
    }
}
