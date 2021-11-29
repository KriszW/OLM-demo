using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingData.SharedAPIModels.Response
{
    public class RoutingDataResponseViewModel
    {
        public RoutingDataResponseViewModel() { }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public IEnumerable<RoutingDataDimensionResponseViewModel> Data { get; set; }
    }
}
