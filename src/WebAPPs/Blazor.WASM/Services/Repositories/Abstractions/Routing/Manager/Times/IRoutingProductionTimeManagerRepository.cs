using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Routing.SharedAPIModels.Response.DataModel;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times
{
    public interface IRoutingProductionTimeManagerRepository
    {
        Task<APIResponse<Paginated<ProductionTimeDataViewModel>>> GetPaginatedData(int year, int weekNumber, int pageIndex, int pageSize);
        Task<APIResponse<ProductionTimeDataViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(ProductionTimeDataViewModel model);
        Task<EmptyAPIResponse> Modify(int id, ProductionTimeDataViewModel model);


        Task<APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>> GetDataPaginatedForWeeks(int pageIndex, int pageSize);
    }
}
