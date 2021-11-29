using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting.Actions
{
    public class FetchDailyRoutingSucceededAction
    {
        public FetchDailyRoutingSucceededAction(string machineName, RoutingResponseViewModel data)
        {
            MachineName = machineName;
            Data = data;
        }

        public string MachineName { get; set; }
        public RoutingResponseViewModel Data { get; set; }
    }
}
