using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing
{
    public class AggregatedRoutingViewModel
    {
        public AggregatedRoutingViewModel() { }

        public RoutingResponseViewModel Daily { get; set; }

        public RoutingResponseViewModel Weekly { get; set; }
    }
}
