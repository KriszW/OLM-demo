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
    public class LoadingBundlePriceEffect : Effect<LoadBundlePricesAction>
    {
        private readonly IBundlePriceRepository _tcoBundlePriceRepository;
        private readonly ILogger<LoadingBundlePriceEffect> _logger;

        public LoadingBundlePriceEffect(IBundlePriceRepository tcoBundlePriceRepository,
                                        ILogger<LoadingBundlePriceEffect> logger)
        {
            _tcoBundlePriceRepository = tcoBundlePriceRepository;
            _logger = logger;
        }

        protected async override Task HandleAsync(LoadBundlePricesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _tcoBundlePriceRepository.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchBundlePricesSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchBundlePricesFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchBundlePricesFailedAction(new APIError(msg)));
            }
        }
    }
}
