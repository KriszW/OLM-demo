using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Target.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Target
{
    public interface IWasteTargetManagerRepository
    {
        Task<APIResponse<Paginated<WasteTargetViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<WasteTargetViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(WasteTargetViewModel model);
        Task<EmptyAPIResponse> Modify(int id, WasteTargetViewModel model);
    }
}
