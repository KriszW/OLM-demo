using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions
{
    public class WasteTargetPageSizeChangedAction
    {
        public WasteTargetPageSizeChangedAction(int newPageSize)
        {
            NewPageSize = newPageSize;
        }

        public int NewPageSize { get; private set; }
    }
}
