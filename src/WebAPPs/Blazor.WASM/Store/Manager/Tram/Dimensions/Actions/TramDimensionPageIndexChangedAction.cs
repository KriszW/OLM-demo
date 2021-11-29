using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions
{
    public class TramDimensionPageIndexChangedAction
    {
        public TramDimensionPageIndexChangedAction(int newPageIndex)
        {
            NewPageIndex = newPageIndex;
        }

        public int NewPageIndex { get; private set; }
    }
}
