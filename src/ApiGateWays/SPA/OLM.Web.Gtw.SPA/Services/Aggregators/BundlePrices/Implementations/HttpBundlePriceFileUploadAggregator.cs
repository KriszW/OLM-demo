using Microsoft.AspNetCore.Http;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.MoneyExchange;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Implementations
{
    public class HttpBundlePriceFileUploadAggregator : IBundlePriceFileUploadAggregator
    {
        private readonly IBundlePriceFileUploadService _bundlePriceFileUploadService;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public HttpBundlePriceFileUploadAggregator(IBundlePriceFileUploadService bundlePriceFileUploadService,
                                                   ICurrencyExchangeService currencyExchangeService)
        {
            _bundlePriceFileUploadService = bundlePriceFileUploadService;
            _currencyExchangeService = currencyExchangeService;
        }

        public Task<EmptyAPIResponse> Upload(IFormFile file, string destinationCurrency)
        {
            throw new NotImplementedException();
        }

        public async Task<EmptyAPIResponse> UploadPowerBI(IFormFile file, ExchangeCurrencyViewModel model)
        {
            var rate = await _currencyExchangeService.Exchange(model);

            if (rate == default) return default;

            return await _bundlePriceFileUploadService.Upload(file, new BundlePriceFileUploadViewModel { Rate = rate.Rate, Currency = model.DestinationCurrency });
        }
    }
}
