using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Utilities.APIEndpointBuilder
{
    public static partial class Endpoints
    {
        public static class MoneyExchangeRate
        {
            //public const string APIVersion = "v1/";
            public const string APIVersion = "";

            public static class Exchange
            {
                public static string BuildExchange(string baseURL)
                    => BuildBaseUrlWithVersioning(baseURL, APIVersion) + $"exchange";
            }
        }
    }
}
