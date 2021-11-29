using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.MoneyExchangeRateManager.CurrencyExchangeRates.Actions
{
    public class LoadExchangeRatesAction
    {
        public LoadExchangeRatesAction(string iSOCode,
                                       int pageIndex,
                                       int pageSize)
        {
            ISOCode = iSOCode;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public string ISOCode { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
