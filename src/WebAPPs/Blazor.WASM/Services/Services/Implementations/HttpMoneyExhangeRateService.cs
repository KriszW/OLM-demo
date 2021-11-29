using OLM.Blazor.WASM.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Services.Implementations
{
    public class HttpMoneyExhangeRateService : IMoneyExchangeService
    {
        public Task<decimal> Exchange(string sourceISOCode, string destISOCode)
        {
            throw new NotImplementedException();
        }
    }
}
