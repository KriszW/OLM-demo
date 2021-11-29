using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingTime.SharedAPIModels.Response
{
    public class RoutingTimesDataResponseViewModel
    {
        public RoutingTimesDataResponseViewModel() { }

        public int AllTime { get; set; }

        public int ProductionMinutes { get; set; }

        public int PauseMinutes { get; set; }

        public string Dimension { get; set; }
    }
}
