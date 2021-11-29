using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class DailyReport
        {
            private const string APIVersion = "";

            public static class Daily
            {
                public static string BuildDailyFetchUrl(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/daily/day/fetch";

                public static string BuildWeekFetchUrl(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/daily/week/fetch";
            }

            public static class Weekly
            {
                public static string BuildWeekly(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/weekly/fetch";
            }

            public static class Weeks
            {
                public static string BuildWeeks(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/weeks/fetch";
            }

            public static class FileDownload
            {
                public static string BuildDimDaily(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/files/dimension/daily";

                public static string BuildDimWeekly(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/files/dimension/weekly";

                public static string BuildWeekly(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/files/weekly";

                public static string BuildWeeks(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"report/files/weeks";
            }
        }
    }
}
