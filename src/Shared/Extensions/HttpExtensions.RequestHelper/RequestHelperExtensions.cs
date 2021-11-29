using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OLM.Shared.Extensions.HttpExtensions.RequestHelper
{
    public static class RequestHelperExtensions
    {
        public static async Task<TValue> GetWithJsonAsync<TValue>(this HttpClient client, string requestUri)
        {
            UriBuilder uri;
            if (client.BaseAddress == default) uri = new UriBuilder(requestUri);
            else uri = new UriBuilder(client.BaseAddress.AbsoluteUri + requestUri);

            var requestContent = new HttpRequestMessage
            {
                RequestUri = uri.Uri
            };

            try
            {
                using var response = await client.SendAsync(requestContent);
                return await response.Content.ReadFromJsonAsync<TValue>();
            }
            catch (Exception ex) when (ex is JsonException)
            {
                return default;
            }
        }
    }
}
