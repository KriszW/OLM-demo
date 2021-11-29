using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions
{
    public class WasteTargetPageIndexChangedAction
    {
        public WasteTargetPageIndexChangedAction(int newPageIndex)
        {
            NewPageIndex = newPageIndex;
        }

        public int NewPageIndex { get; private set; }
    }
}
