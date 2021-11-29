using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingTime.SharedAPIModels.Request
{
    public class FetchRoutingRequestTimeViewModel
    {
        public FetchRoutingRequestTimeViewModel() { }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string MachineName { get; set; }
    }
}
