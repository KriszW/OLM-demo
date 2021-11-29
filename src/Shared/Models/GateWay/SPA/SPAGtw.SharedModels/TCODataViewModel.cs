using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels
{
    public class TCODataViewModel
    {
        [JsonConstructor]
        public TCODataViewModel(double expected, double realValue, double maximumDifference)
        {
            Expected = expected;
            RealValue = realValue;
            MaximumDifference = maximumDifference;
        }

        public double Expected { get; set; }

        public double RealValue { get; set; }

        public double MaximumDifference { get; set; }
    }
}
