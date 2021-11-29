using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Actions
{
    public class StartFetchingWeeklyRoutingAction
    {
        public StartFetchingWeeklyRoutingAction(string machineName)
        {
            MachineName = machineName;
        }

        public string MachineName { get; set; }
    }
}
