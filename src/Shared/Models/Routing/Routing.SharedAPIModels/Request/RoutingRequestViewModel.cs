using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Routing.SharedAPIModels.Request
{
    public class RoutingRequestViewModel
    {

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string MachineName { get; set; }
    }
}
