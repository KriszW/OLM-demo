using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing
{
    public interface IRoutingRepository
    {
        Task<APIResponse<RoutingResponseViewModel>> FetchForDay(string machineID);
        Task<APIResponse<RoutingResponseViewModel>> FetchForWeek(string machineID);


        Task<APIResponse<AggregatedRoutingViewModel>> Fetch(string machineID);
    }
}
