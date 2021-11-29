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
using OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Effects
{
    public class LoadExchangeRatesEffect : Effect<LoadExchangeRatesAction>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly ILogger<LoadExchangeRatesEffect> _logger;

        public LoadExchangeRatesEffect(IExchangeRateRepository exchangeRateRepository,
                                      ILogger<LoadExchangeRatesEffect> logger)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadExchangeRatesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _exchangeRateRepository.GetPaginatedData(action.ISOCode, action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchExchangeRatesSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchExchangeRatesFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchExchangeRatesFailedAction(new APIError(msg)));
            }
        }
    }
}
