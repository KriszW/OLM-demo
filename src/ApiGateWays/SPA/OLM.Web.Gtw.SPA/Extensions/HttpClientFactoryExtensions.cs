using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Extensions
{
    public static class HttpClientFactoryExtensions
    {
        public static HttpClient CreateClientForServices(this IHttpClientFactory clientFactory)
            => clientFactory.CreateClient(Startup.DefaultAPIServicesHTTPClientName);
    }
}
