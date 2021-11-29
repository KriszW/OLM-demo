using OLM.Services.RoutingTime.API.Models;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Repositories.Abstractions
{
    public interface IBundlesRepository
    {
        Task<List<BundleModel>> FetchAll(string machineName, DateTime start, DateTime end);
    }
}
