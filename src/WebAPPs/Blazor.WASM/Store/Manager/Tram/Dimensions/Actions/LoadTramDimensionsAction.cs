using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions
{
    public class LoadTramDimensionsAction
    {
        public LoadTramDimensionsAction(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
