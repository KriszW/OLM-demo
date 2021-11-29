using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions
{
    public class PausePageIndexChangedAction
    {
        public PausePageIndexChangedAction(int newPageIndex)
        {
            NewPageIndex = newPageIndex;
        }

        public int NewPageIndex { get; private set; }
    }
}
