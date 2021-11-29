using Microsoft.AspNetCore.Http;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions
{
    public interface IBundlePriceFileUploadAggregator
    {
        Task<EmptyAPIResponse> UploadPowerBI(IFormFile file, ExchangeCurrencyViewModel model);

        Task<EmptyAPIResponse> Upload(IFormFile file, string destinationCurrency);
    }
}
