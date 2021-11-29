using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels
{
    public class CurrencyViewModel
    {
        public int? ID { get; set; }
        public string ISOCode { get; set; }
        public List<ExchangeRateViewModel> Rates { get; set; }
    }
}
