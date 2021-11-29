using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Services.Abstractions
{
    public interface IMoneyExchangeService
    {
        Task<decimal> Exchange(string sourceISOCode, string destISOCode);
    }
}
