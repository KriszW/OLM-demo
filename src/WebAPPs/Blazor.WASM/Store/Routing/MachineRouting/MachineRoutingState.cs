using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting
{
    public class MachineRoutingState
    {
        public MachineRoutingState(string machineName) : this(machineName, default, true, default) { }

        public MachineRoutingState(string machineName, string errorMSG) : this(machineName, errorMSG, false, default) { }
        public MachineRoutingState(string machineName, AggregatedRoutingViewModel data) : this(machineName, default, false, data) { }

        public MachineRoutingState(string machineName,
                                   string errorMSG,
                                   bool isLoading,
                                   AggregatedRoutingViewModel data)
        {
            MachineName = machineName;
            ErrorMSG = errorMSG;
            IsLoading = isLoading;
            Data = data;
        }

        public string MachineName { get; set; }

        public string ErrorMSG { get; set; }

        public bool IsLoading { get; set; }

        public AggregatedRoutingViewModel Data { get; set; }
    }
}
