using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;

namespace OLM.Services.Bundles.Prices.API.Extensions
{
    public static class StartupHttpClientExtensions
    {
        // TODO: Polly hozzáadása az HttpClientFactoryhoz
        public static IServiceCollection AddAPIServicesHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient(Startup.DefaultAPIServicesHTTPClientName, (sp, client) =>
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
