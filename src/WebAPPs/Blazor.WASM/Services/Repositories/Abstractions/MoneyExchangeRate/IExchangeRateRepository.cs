using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.ExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate
{
    public interface IExchangeRateRepository
    {
        Task<APIResponse<Paginated<ExchangeRateViewModel>>> GetPaginatedData(string isoCode, int pageIndex, int pageSize);
        Task<APIResponse<ExchangeRateViewModel>> GetByID(int id);
        Task<EmptyAPIResponse> Delete(int id);
        Task<EmptyAPIResponse> Upload(UploadNewExchangeRateForISOCodeViewModel model);
        Task<EmptyAPIResponse> Modify(string sourceISOCode, ExchangeRateViewModel model);
    }
}
