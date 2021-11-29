using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Target;
using OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Effects
{
    public class LoadCurrenciesEffect : Effect<LoadCurrenciesAction>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ILogger<LoadCurrenciesEffect> _logger;

        public LoadCurrenciesEffect(ICurrencyRepository currencyRepository,
                                      ILogger<LoadCurrenciesEffect> logger)
        {
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadCurrenciesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _currencyRepository.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchCurrenciesSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchCurrenciesFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchCurrenciesFailedAction(new APIError(msg)));
            }
        }
    }
}
