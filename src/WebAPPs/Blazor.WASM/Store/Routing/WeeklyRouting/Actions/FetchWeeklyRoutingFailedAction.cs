using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Actions
{
    public class FetchWeeklyRoutingFailedAction
    {
        public FetchWeeklyRoutingFailedAction(string machineName, string errorMSG)
        {
            MachineName = machineName;
            ErrorMSG = errorMSG;
        }

        public string MachineName { get; set; }

        public string ErrorMSG { get; set; }
    }
}
