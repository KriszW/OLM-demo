using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Extensions
{
    public static class DoubleExtensions
    {
        public static double Normalize(this double value) 
            => double.IsNaN(value) || double.IsInfinity(value) ? 0 : value;
    }
}
