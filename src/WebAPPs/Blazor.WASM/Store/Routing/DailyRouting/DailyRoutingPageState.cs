using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting
{
    public class DailyRoutingPageState
    {
        public DailyRoutingPageState(string machineName) : this(machineName, default, true, default) { }

        public DailyRoutingPageState(string machineName, string errorMSG) : this(machineName, errorMSG, false, default) { }
        public DailyRoutingPageState(string machineName, RoutingResponseViewModel data) : this(machineName, default, false, data) { }

        public DailyRoutingPageState(string machineName,
                                     string errorMSG,
                                     bool isLoading,
                                     RoutingResponseViewModel data)
        {
            MachineName = machineName;
            ErrorMSG = errorMSG;
            IsLoading = isLoading;
            Data = data;
        }

        public string MachineName { get; set; }

        public string ErrorMSG { get; set; }

        public bool IsLoading { get; set; }

        public RoutingResponseViewModel Data { get; set; }
    }
}
