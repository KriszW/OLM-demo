using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class PageSizeChangedAction
    {
        public PageSizeChangedAction(int newPageSize)
        {
            NewPageSize = newPageSize;
        }

        public int NewPageSize { get; private set; }
    }
}
