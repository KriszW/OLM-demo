using OLM.Services.Routing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Routing.API.Services.Repositories.Abstractions
{
    public interface IRoutingRepository
    {
        Task<List<RoutingModel>> Fetch(IEnumerable<string> dimension);
    }
}
