using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.DailyReport
{
    [Route("api/bundles/prices/files/upload")]
    [ApiController]
    public class BundlePriceFileUploadController : ControllerBase
    {
        private readonly IBundlePriceFileUploadAggregator _bundlePriceFileUploadAggregator;

        public BundlePriceFileUploadController(IBundlePriceFileUploadAggregator bundlePriceFileUploadAggregator)
        {
            _bundlePriceFileUploadAggregator = bundlePriceFileUploadAggregator;
        }

        [HttpPost]
        [Route("powerbi")]
        public async Task<ActionResult<EmptyAPIResponse>> UploadPowerBI([FromForm] IFormFile file, [FromQuery] ExchangeCurrencyViewModel model)
        {
            var result = await _bundlePriceFileUploadAggregator.UploadPowerBI(file, model);

            if (result != default)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new EmptyAPIResponse("A feltöltés sikertelen volt"));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<EmptyAPIResponse>> Upload([FromForm] IFormFile file, [FromQuery] string destinationCurrency)
        {
            var result = await _bundlePriceFileUploadAggregator.Upload(file, destinationCurrency);

            if (result != default)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new EmptyAPIResponse("A feltöltés sikertelen volt"));
            }
        }
    }
}
