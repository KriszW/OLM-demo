using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Bundle
{
    public interface ITCOBundleRepository
    {
        Task<APIResponse<Paginated<TCOBundleAPIResponseViewModel>>> GetData(DateTime from, DateTime to, int pageIndex, int pageSize);
    }
}
