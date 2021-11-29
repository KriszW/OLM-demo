using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OLM.Shared.Models.TCO.SharedAPIModels.TCO
{
    public class BundleTCOAPIResponseViewModel
    {
        [JsonConstructor]
        public BundleTCOAPIResponseViewModel(double calculatedValue, double expectedTCOValue, double maximumDifference)
        {
            CalculatedValue = calculatedValue;
            ExpectedTCOValue = expectedTCOValue;
            MaximumDifference = maximumDifference;
        }

        public double CalculatedValue { get; set; }
        public double ExpectedTCOValue { get; set; }
        public double MaximumDifference { get; set; }
    }
}
