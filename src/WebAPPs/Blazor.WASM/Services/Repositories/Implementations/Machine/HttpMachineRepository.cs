using OLM.Blazor.WASM.Services.Repositories.Abstractions.Machine;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.HttpExtensions.RequestHelper;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Machine
{
    public class HttpMachineRepository : IMachineRepository
    {
        private readonly HttpClient _httpClient;

        public HttpMachineRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<APIResponse<MachineViewModel>> FetchMachineData(string machineName)
            => _httpClient.GetWithJsonAsync<APIResponse<MachineViewModel>>(APIGTWEndpoints.Machine.GetMachineData(machineName));

        public Task<APIResponse<SummarizedMachineViewModel>> FetchSummarizedData()
            => _httpClient.GetWithJsonAsync<APIResponse<SummarizedMachineViewModel>>(APIGTWEndpoints.Machine.GetMachinesData());
    }
}
