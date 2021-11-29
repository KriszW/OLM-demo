using OLM.Services.Tram.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Services.Repositories.Abstractions
{
    public interface ITramDataRepository
    {
        Task<Paginated<TramDataModel>> GetPaginetedPrices(int skip, int take);

        Task<TramDataModel> GetByID(int id);

        Task Add(TramDataModel model);
        Task Modify(int id, TramDataModel model);
        Task Delete(int id);
    }
}
