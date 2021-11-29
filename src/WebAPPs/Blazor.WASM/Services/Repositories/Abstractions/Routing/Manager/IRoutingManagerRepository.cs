using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager
{
    public interface IRoutingManagerRepository
    {
        Task<APIResponse<Paginated<RoutingDataViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<RoutingDataViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(RoutingDataViewModel model);
        Task<EmptyAPIResponse> Modify(int id, RoutingDataViewModel model);
    }
}
