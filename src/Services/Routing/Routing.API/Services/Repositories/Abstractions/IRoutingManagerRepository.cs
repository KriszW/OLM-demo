using OLM.Services.Routing.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Repositories.Abstractions
{
    public interface IRoutingManagerRepository
    {
        Task<Paginated<RoutingModel>> GetPaginated(int skip, int take);

        Task<RoutingModel> GetByID(int id);

        Task Add(RoutingModel model);
        Task Modify(int id, RoutingModel model);
        Task Delete(int id);
    }
}
