using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.MoneyExchange
{
    public interface IMoneyExchangeService
    {
        Task<double> Exchange(double value, string sourceCurr, string destCurr);
        Task<decimal> Exchange(decimal value, string sourceCurr, string destCurr);
    }
}
