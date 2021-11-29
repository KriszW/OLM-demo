using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Routing.SharedAPIModels.Response
{
    public class RoutingResponseViewModel
    {
        public RoutingResponseViewModel() { }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public IEnumerable<RoutingsDataResponseViewModel> Data { get; set; }
    }
}
