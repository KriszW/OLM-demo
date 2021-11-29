using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.ViewModels
{
    public class PaginatedExchangeRateViewModel
    {
        public PaginatedExchangeRateViewModel(string isoCode, int skip, int take)
        {
            ISOCode = isoCode;
            Skip = skip;
            Take = take;
        }

        public string ISOCode { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
