using OLM.Services.MoneyExchangeRate.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.ViewModels
{
    public class ModifyExchangeRateForCurrencyViewModel
    {
        public string SourceISOCode { get; set; }

        public ExchangeRateModel Data { get; set; }
    }
}
