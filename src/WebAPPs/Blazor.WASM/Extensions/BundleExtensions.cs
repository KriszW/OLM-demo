using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Extensions
{
    public static class BundleExtensions
    {
        public static string GetTCOLightbulbType(this TCODataViewModel tco)
            => tco.RealValue.IsInAcceptableRange(tco.Expected, tco.MaximumDifference) ? "green" : "red";
    }
}
