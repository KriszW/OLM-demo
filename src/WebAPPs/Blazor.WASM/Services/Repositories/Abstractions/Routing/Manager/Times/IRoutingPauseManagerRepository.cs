using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times
{
    public interface IRoutingPauseManagerRepository
    {
        Task<APIResponse<Paginated<PauseDataViewModel>>> GetPaginatedData(int year, int weekNumber, int pageIndex, int pageSize);
        Task<APIResponse<PauseDataViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(PauseDataViewModel model);
        Task<EmptyAPIResponse> Modify(int id, PauseDataViewModel model);


        Task<APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>> GetDataPaginatedForWeeks(int pageIndex, int pageSize);
    }
}
