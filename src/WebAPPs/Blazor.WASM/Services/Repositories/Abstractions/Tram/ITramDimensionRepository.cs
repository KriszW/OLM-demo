using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram
{
    public interface ITramDimensionRepository
    {
        Task<APIResponse<Paginated<TramDimensionViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<TramDimensionViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(TramDimensionViewModel model);
        Task<EmptyAPIResponse> Modify(int id, TramDimensionViewModel model);


        Task<APIResponse<IEnumerable<string>>> GetAllDimensions();
    }
}
