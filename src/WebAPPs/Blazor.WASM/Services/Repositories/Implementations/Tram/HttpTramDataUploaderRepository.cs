using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Blazor.WASM.ViewModels.Tram;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Tram
{
    public class HttpTramDataUploaderRepository : ITramDataUploaderRepository
    {
        private readonly HttpClient _httpClient;

        public HttpTramDataUploaderRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmptyAPIResponse> Upload(TramDataEditViewModel model)
        {
            var route = APIGTWEndpoints.Tram.DataUploader.Upload();

            var utcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);

            var realModel = new TramDataViewModel
            {
                Date = model.Date.Add(utcOffset),
                MachineID = model.MachineID,
                Dimension = new TramDimensionViewModel { Dimension = model.Dimension },
                NumberOfLamella = model.NumberOfLamella,
                NumberOfTrams = model.NumberOfTrams,
                Shift = model.Shift,
            };

            var msg = await _httpClient.PostAsJsonAsync(route, realModel);

            return await msg.Content.ReadFromJsonAsync<EmptyAPIResponse>();
        }
    }
}
