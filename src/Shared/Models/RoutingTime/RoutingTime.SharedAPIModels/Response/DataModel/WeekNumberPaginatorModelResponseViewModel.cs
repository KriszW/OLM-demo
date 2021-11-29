using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel
{
    public class WeekNumberPaginatorModelDataViewModel
    {
        public WeekNumberPaginatorModelDataViewModel() { }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int WeekNumber { get; set; }

        public int Year { get; set; }
    }
}
