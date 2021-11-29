using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate
{
    public interface ICurrencyRepository
    {
        Task<APIResponse<List<string>>> GetAll();
        Task<APIResponse<Paginated<CurrencyViewModel>>> GetPaginatedData(int pageIndex, int pageSize);
        Task<APIResponse<CurrencyViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(CurrencyViewModel model);
        Task<EmptyAPIResponse> Modify(int id, CurrencyViewModel model);
    }
}
