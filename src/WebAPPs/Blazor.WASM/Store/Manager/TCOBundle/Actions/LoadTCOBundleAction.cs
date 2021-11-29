using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions
{
    public class LoadTCOBundleAction
    {
        public LoadTCOBundleAction(DateTime from, DateTime to, int pageIndex, int pageSize)
        {
            From = from;
            To = to;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
