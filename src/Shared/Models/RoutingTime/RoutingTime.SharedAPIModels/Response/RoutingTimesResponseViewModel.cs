using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingTime.SharedAPIModels.Response
{
    public class RoutingTimesResponseViewModel
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public IEnumerable<RoutingTimesDataResponseViewModel> Data { get; set; }
    }
}
