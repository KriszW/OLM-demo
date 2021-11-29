using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel
{
    public class ProductionTimeDataViewModel
    {
        public int? ID { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DayOfWeek Day { get; set; }

        public int WeekNumber { get; set; }

        public string MachineName { get; set; }
    }
}
