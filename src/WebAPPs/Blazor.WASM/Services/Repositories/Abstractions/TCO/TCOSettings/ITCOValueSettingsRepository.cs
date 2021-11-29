using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.TCOSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.TCO.TCOSettings
{
    public interface ITCOValueSettingsRepository
    {
        Task<APIResponse<Paginated<TCOSettingsViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<TCOSettingsViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(TCOSettingsViewModel model);
        Task<EmptyAPIResponse> Modify(int id, TCOSettingsViewModel model);
    }
}
