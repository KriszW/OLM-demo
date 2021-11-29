using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class CategoryBulbs
        {
            private const string APIVersion = "";

            public static class Validator
            {
                public static string BuildValidatorURL(string baseURL, string bundleID)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"validation/bundle/{bundleID}";
            }
        }
    }
}
