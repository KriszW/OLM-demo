using OLM.Services.Target.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Target.API.Services.Repositories.Abstractions
{
    public interface IWasteTargetRepository
    {
        Task<Paginated<WasteTargetDataModel>> Paginate(int skip, int take);

        Task<WasteTargetDataModel> GetByID(int id);

        Task Add(WasteTargetDataModel model);
        Task Modify(int id, WasteTargetDataModel model);
        Task Delete(int id);
    }
}
