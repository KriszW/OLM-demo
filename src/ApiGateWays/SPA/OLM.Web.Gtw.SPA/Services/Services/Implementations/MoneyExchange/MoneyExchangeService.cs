using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.MoneyExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Implementations.MoneyExchange
{
    public class MoneyExchangeService : IMoneyExchangeService
    {
        public Task<double> Exchange(double value, string sourceCurr, string destCurr)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> Exchange(decimal value, string sourceCurr, string destCurr)
        {
            throw new NotImplementedException();
        }
    }
}
