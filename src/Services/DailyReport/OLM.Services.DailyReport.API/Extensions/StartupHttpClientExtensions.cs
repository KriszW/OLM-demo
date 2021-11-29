using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Extensions
{
    public static class StartupHttpClientExtensions
    {
        public const string DefaultAPIServicesHTTPClientName = "services";

        public static HttpClient CreateClientForServices(this IHttpClientFactory clientFactory)
            => clientFactory.CreateClient(DefaultAPIServicesHTTPClientName);

        public static IServiceCollection AddAPIServicesHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient(DefaultAPIServicesHTTPClientName, (sp, client) =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>();

                var request = httpContext.HttpContext.Request;

                client.BaseAddress = default;

                var authHeaderValueResult = request.Headers.TryGetValue("Authorization", out StringValues output);

                if (authHeaderValueResult == true)
                {
                    var data = output[0].Split(' ', 2);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(data[0], data[1]);
                }
            });


            return services;
        }
    }
}
