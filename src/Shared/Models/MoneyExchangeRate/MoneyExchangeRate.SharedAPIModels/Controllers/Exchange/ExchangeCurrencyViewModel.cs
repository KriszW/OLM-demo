using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange
{
    public class ExchangeCurrencyViewModel
    {
        public ExchangeCurrencyViewModel() { }

        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
    }
}
