using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public const string APIVersion = "";

        public static class Routing
        {
            public static string GetRoutingCalculater(string baseURL)
                => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"routing/calculate";
        }
    }
}
