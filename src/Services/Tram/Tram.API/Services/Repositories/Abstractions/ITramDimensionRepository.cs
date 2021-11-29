using OLM.Services.Tram.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Services.Repositories.Abstractions
{
    public interface ITramDimensionRepository
    {
        Task<List<string>> GetAllDimension();

        Task<Paginated<TramDimensionModel>> GetPagineted(int skip, int take);

        Task<TramDimensionModel> GetByID(int id);

        Task Add(TramDimensionModel model);
        Task Modify(int id, TramDimensionModel model);
        Task Delete(int id);
    }
}
