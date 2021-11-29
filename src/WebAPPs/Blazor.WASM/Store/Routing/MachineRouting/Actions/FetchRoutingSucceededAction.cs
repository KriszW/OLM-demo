using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions
{
    public class FetchRoutingSucceededAction
    {
        public FetchRoutingSucceededAction(string machineName, AggregatedRoutingViewModel data)
        {
            MachineName = machineName;
            Data = data;
        }

        public string MachineName { get; set; }
        public AggregatedRoutingViewModel Data { get; set; }
    }
}
