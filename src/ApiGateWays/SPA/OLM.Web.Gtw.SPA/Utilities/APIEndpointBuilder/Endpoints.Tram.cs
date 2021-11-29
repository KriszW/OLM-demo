using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class Tram
        {
            private const string APIVersion = "";

            public static class Trams
            {
                public static string BuildFetch(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"trams/fetch";
            }
        }
    }
}
