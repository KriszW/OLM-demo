using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class BundlePrices
        {
            //public const string APIVersion = "v1/";
            public const string APIVersion = "";

            public static class BundleIDPrice
            {
                public static string GetForOne(string baseURL, string bundleID)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"api/bundle/{bundleID}/prices";
                public static string GetForMany(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundles/prices";
            }

            public static class Upload
            {
                public static string BuildForUpload(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"bundle/prices/file/manager/upload";
            }
        }
    }
}
