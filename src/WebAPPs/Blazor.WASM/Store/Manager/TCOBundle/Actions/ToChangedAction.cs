using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions
{
    public class ToChangedAction
    {
        public ToChangedAction(DateTime to)
        {
            To = to;
        }

        public DateTime To { get; private set; }
    }
}
