using Microsoft.AspNetCore.Http;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions
{
    public interface IBundlePriceManagerRepository
    {
        Task<Paginated<BundlePriceModel>> GetPagineted(int skip, int take);

        Task<BundlePriceModel> GetByID(int id);

        Task Upload(BundlePriceModel model);
        Task Modify(int id, BundlePriceModel model);
        Task Delete(int id);
    }
}
