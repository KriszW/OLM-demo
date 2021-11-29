using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.CategoryBulbs.APIResponses.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.CategoryBulbs.Manager
{
    public interface ICategoryBulbsSettingsRepository
    {
        Task<APIResponse<Paginated<CategoryBulbItemNumberSettingsViewModel>>> Search(string categoryQuery, int pageIndex, int pageSize);
        Task<APIResponse<Paginated<CategoryBulbItemNumberSettingsViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<CategoryBulbItemNumberSettingsViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(CategoryBulbItemNumberSettingsViewModel model);
        Task<EmptyAPIResponse> Modify(int id, CategoryBulbItemNumberSettingsViewModel model);
    }
}
