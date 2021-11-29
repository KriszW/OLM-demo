using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting.Actions
{
    public class StartFetchingDailyRoutingAction
    {
        public StartFetchingDailyRoutingAction(string machineName)
        {
            MachineName = machineName;
        }

        public string MachineName { get; set; }
    }
}
