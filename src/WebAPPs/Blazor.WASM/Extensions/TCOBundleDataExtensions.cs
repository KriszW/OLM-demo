using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Extensions
{
    public static class TCOBundleDataExtensions
    {
        public static bool IsInAcceptableRange(this double actual, double expected, double maximumDifference) 
            => (expected - maximumDifference) >= actual && (expected + maximumDifference) <= actual;
    }
}
