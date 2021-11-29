using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Extensions
{
    public static class ViewModelToModelExtensions
    {
        public static ExchangeRateModel ConvertToModel(this ExchangeRateViewModel model)
            => new ExchangeRateModel { ID = model.ID, DestISOCode = model.DestISOCode, Rate = model.Rate };

        public static CurrencyModel ConvertToModel(this CurrencyViewModel model)
            => new CurrencyModel { ID = model.ID, ISOCode = model.ISOCode, Rates = model.Rates.Select(m => m.ConvertToModel()).ToList() };
    }
}
