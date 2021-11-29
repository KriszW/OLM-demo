using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class TCO
        {
            //public const string APIVersion = "v1/";
            public const string APIVersion = "";

            public static class TCOController
            {
                public static string GetTCOData(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"tco/bundle";

                public static string CalculateForOneBundle(string baseURL, string bundleID)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"tco/bundle/{bundleID}";
                public static string CalculateAVGTCO(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"tco/bundles";
            }
            public static class TCOAggregator
            {
                public static string Aggregate(string baseURL, string bundleID)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundle/tco/calc/{bundleID}";
                public static string AggregateAVG(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundles/tco/calc";
            }

            public static class RawTCOAggregator
            {
                public static string Aggregate(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"tcoraw/single";
                public static string AggregateAVG(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"tcoraw/many";
            }
        }
    }
}
