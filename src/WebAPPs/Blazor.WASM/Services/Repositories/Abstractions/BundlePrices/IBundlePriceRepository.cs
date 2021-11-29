using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.TCO.SharedAPIModels.BundlePrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices
{
    public interface IBundlePriceRepository
    {
        Task<APIResponse<Paginated<BundlePriceDTOViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<BundlePriceDTOViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(BundlePriceDTOViewModel model);
        Task<EmptyAPIResponse> Modify(int id, BundlePriceDTOViewModel model);
    }
}
