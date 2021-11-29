using OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.BundlePrices
{
    public class HttpBundlePriceUploadRepository : IBundlePriceUploadRepository
    {
        private readonly HttpClient _httpClient;

        public HttpBundlePriceUploadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Upload(UploadPowerBiFileWithCurrenciesAction model)
        {
            var route = APIGTWEndpoints.BundlePrices.Uploader.BuildUpload() + $"?SourceCurrency={model.SourceCurrency}&DestinationCurrency={model.DestinationCurrency}";

            using var memStream = new MemoryStream();
            await model.File.WriteToStreamAsync(memStream);

            var byteArray = memStream.ToArray();

            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            content.Add(new StreamContent(new MemoryStream(byteArray), (int)model.File.Size), "file", model.File.Name);

            var respone = await _httpClient.PostAsync(route, content);

            return await respone.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
