using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices
{
    public interface IBundlePriceUploadRepository
    {
        Task<EmptyAPIResponse> Upload(UploadPowerBiFileWithCurrenciesAction model);
    }
}
