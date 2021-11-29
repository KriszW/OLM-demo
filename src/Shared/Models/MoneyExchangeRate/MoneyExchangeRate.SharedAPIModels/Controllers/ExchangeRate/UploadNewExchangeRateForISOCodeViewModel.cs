using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.ExchangeRate
{
    public class UploadNewExchangeRateForISOCodeViewModel
    {
        public string SourceISOCode { get; set; }

        public ExchangeRateViewModel Model { get; set; }
    }
}
