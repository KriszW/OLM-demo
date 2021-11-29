using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingData.SharedAPIModels.Request
{
    public class FetchRoutingDataRequestViewModel
    {
        public FetchRoutingDataRequestViewModel() { }

        public string MachineName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
