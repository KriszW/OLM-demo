using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.Settings
{
    public class LivePagesOptions
    {
        public int LiveWaitingTimeBetweenPages { get; set; }

        public IEnumerable<string> Pages { get; set; }
    }
}
