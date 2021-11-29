using CsvHelper.Configuration;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using System.Collections.Generic;
using System.Globalization;

namespace OLM.Services.MoneyExchangeRate.API.Maps
{
    public class ExchangeRateClassMap : ClassMap<ExchangeRateCsvViewModel>
    {
        public ExchangeRateClassMap()
        {
            Map(m => m.Currency).Name("Currency");
            Map(m => m.Rates).Convert(row =>
            {
                var output = new List<RateCsvViewModel>();

                for (var i = 1; i < row.Row.HeaderRecord.Length; i += 2)
                {
                    var text = row.Row.GetField<string>(i + 1).Replace(",", ".");

                    output.Add(new(row.Row.GetField<string>(i), decimal.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture)));
                }

                return output;
            });
        }
    }
}
