using System.Collections.Generic;
using System.Text;

namespace OLM.Services.MoneyExchangeRate.API.ViewModels
{
    public class ExchangeRateCsvViewModel
    {
        public string Currency { get; set; }

        public IList<RateCsvViewModel> Rates { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder(Currency);

            foreach (var item in Rates)
            {
                stringBuilder.Append($";{item.ISOCode};{item.Rate}");
            }

            return stringBuilder.ToString();
        }
    }

    public record RateCsvViewModel(string ISOCode, decimal Rate);
}
