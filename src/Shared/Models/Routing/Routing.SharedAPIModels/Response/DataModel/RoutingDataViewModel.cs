using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel
{
    public class RoutingDataViewModel
    {
        public int? ID { get; set; }

        public string Dimension { get; set; }

        public double CycleQuantityPerMinute { get; set; }
    }
}
