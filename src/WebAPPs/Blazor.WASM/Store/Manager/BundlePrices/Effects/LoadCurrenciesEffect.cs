using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate;
using OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Effects
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

        protected async override Task HandleAsync(LoadCurrenciesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _currencyRepository.GetAll();

                if (response.Success)
                {
                    dispatcher.Dispatch(new LoadCurrenciesSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new LoadCurrenciesSuccessAction(new List<string>()));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A valuták lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new LoadCurrenciesSuccessAction(new List<string>()));
            }
        }
    }
}
