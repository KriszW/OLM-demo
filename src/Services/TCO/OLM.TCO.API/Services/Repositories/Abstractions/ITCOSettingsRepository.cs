using OLM.Services.TCO.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.TCO.API.Services.Repositories.Abstractions
{
    public interface ITCOSettingsRepository
    {
        Task<TCOValueSettingsModel> GetByDimension(string dimension);
        Task<List<TCOValueSettingsModel>> GetForDimensions(IEnumerable<string> dimensions);
        Task<TCOValueSettingsModel> GetByID(int id);

        Task<Paginated<TCOValueSettingsModel>> GetPaginated(int skip, int take);

        Task Modify(int id, TCOValueSettingsModel model);
        Task Add(TCOValueSettingsModel model);
        Task Delete(int id);
    }
}
