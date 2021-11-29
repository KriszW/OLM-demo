using OLM.Services.Bundles.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle
{
    public interface IBundlesSumRepository
    {
        Task<List<BundleModel>> GetDailySumBundles();
        Task<List<BundleModel>> GetWeeklySumBundles();
    }
}
