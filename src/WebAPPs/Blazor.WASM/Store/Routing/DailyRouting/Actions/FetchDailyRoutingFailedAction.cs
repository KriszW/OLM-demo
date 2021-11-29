using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting.Actions
{
    public class FetchDailyRoutingFailedAction
    {
        public FetchDailyRoutingFailedAction(string machineName, string errorMSG)
        {
            MachineName = machineName;
            ErrorMSG = errorMSG;
        }

        public string MachineName { get; set; }

        public string ErrorMSG { get; set; }
    }
}
