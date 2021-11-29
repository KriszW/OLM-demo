using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.Currency.Actions
{
    public class FetchCurrenciesFailedAction
    {
        public FetchCurrenciesFailedAction(APIError errors)
        {
            Errors = errors;
        }

        public APIError Errors { get; set; }
    }
}
