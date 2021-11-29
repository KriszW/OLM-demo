using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions
{
    public interface IExchangeRateRepository
    {
        Task<ExchangeRateModel> GetByID(int id);

        Task<Paginated<ExchangeRateModel>> GetPaginatedForISOCode(PaginatedExchangeRateViewModel model);

        Task Modify(ModifyExchangeRateForCurrencyViewModel model);

        Task Add(string ISOCode, ExchangeRateModel model);

        Task Delete(int id);
    }
}
