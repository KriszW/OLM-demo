using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Bundle;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using OLM.Blazor.WASM.Store.Manager.TCOBundle.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle.Effects
{
    public class LoadingTCOBundleEffect : Effect<LoadTCOBundleAction>
    {
        private readonly ITCOBundleRepository _tcoBundleRepository;
        private readonly ILogger<LoadingTCOBundleEffect> _logger;

        public LoadingTCOBundleEffect(ITCOBundleRepository tcoBundleRepository,
                                      ILogger<LoadingTCOBundleEffect> logger)
        {
            _tcoBundleRepository = tcoBundleRepository;
            _logger = logger;
        }

        protected async override Task HandleAsync(LoadTCOBundleAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _tcoBundleRepository.GetData(action.From, action.To, action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchTCOBundleSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchTCOBundleFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchTCOBundleFailedAction(new APIError(msg)));
            }
        }
    }
}
