using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Effects
{
    public class UploadBundlePriceFileEffect : Effect<UploadPowerBiFileWithCurrenciesAction>
    {
        private readonly IBundlePriceUploadRepository _bundlePriceUploadService;
        private readonly ILogger<UploadBundlePriceFileEffect> _logger;

        public UploadBundlePriceFileEffect(IBundlePriceUploadRepository bundlePriceUploadService,
                                           ILogger<UploadBundlePriceFileEffect> logger)
        {
            _bundlePriceUploadService = bundlePriceUploadService;
            _logger = logger;
        }

        protected async override Task HandleAsync(UploadPowerBiFileWithCurrenciesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _bundlePriceUploadService.Upload(action);

                if (response.Success == true)
                {
                    dispatcher.Dispatch(new UploadBundlePriceFileFinishedAction("A fájlt sikeresen feltöltötted, és frissítetted az adatokat"));
                }
                else
                {
                    dispatcher.Dispatch(new UploadBundlePriceFileFinishedAction(response.Message));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A fájl feltöltése közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new UploadBundlePriceFileFinishedAction(msg));
            }
        }
    }
}
