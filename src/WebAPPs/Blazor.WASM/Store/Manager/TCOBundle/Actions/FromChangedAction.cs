using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions
{
    public class FromChangedAction
    {
        public FromChangedAction(DateTime from)
        {
            From = from;
        }

        public DateTime From { get; private set; }
    }
}
