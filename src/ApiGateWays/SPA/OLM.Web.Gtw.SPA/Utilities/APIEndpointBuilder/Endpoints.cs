using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        private static string BuildBaseUrlWithVersioning(string baseURL, string apiVersion)
            => baseURL + "api/" + apiVersion;
    }
}
