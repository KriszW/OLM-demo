using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels
{
    public class ExchangeRateViewModel
    {
        public int? ID { get; set; }
        public string DestISOCode { get; set; }
        public decimal Rate { get; set; }
    }
}
