using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle
{
    public interface ITCOBundleRepository
    {
        Task<IEnumerable<TCOBundleAPIResponseViewModel>> FetchData(DateTime from, DateTime to);
        Task<Paginated<TCOBundleAPIResponseViewModel>> FetchData(DateTime from, DateTime to, int skip, int take);
    }
}
