using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Routing.SharedAPIModels.Response
{
    public class RoutingsDataResponseViewModel
    {
        public RoutingsDataResponseViewModel() { }

        public RoutingsDataResponseViewModel(string dimension,
                                            double expectedRouting,
                                            double actualRouting)
        {
            Dimension = dimension;
            ExpectedRouting = expectedRouting;
            ActualRouting = actualRouting;
        }

        public string Dimension { get; set; }

        public double ExpectedRouting { get; set; }

        public double ActualRouting { get; set; }
    }
}
