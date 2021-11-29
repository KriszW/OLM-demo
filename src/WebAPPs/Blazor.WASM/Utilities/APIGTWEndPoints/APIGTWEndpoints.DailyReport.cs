using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.APIGTWEndPoints
{
    public static partial class APIGTWEndpoints
    {
        public static class DailyReport
        {
            public static string FetchDimForDay()
                => BuildBaseURL() + $"dailyreports/dimension/day";

            public static string FetchDimForWeek()
                => BuildBaseURL() + $"dailyreports/dimension/week";

            public static string FetchWeeklyDay()
                => BuildBaseURL() + $"dailyreports/weekly";

            public static string FetchWeeks()
                => BuildBaseURL() + $"dailyreports/weeks";
        }
    }
}
