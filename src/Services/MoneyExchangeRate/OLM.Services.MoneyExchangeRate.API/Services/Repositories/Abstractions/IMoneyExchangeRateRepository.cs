using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions
{
    public interface IMoneyExchangeRateRepository
    {
        Task<CurrencyModel> GetByID(int id);
        Task<Paginated<CurrencyModel>> GetPaginated(int skip, int take);

        Task Modify(int id, CurrencyModel model);
        Task Add(CurrencyModel model);
        Task Delete(int id);

        Task<List<string>> All();
    }
}
