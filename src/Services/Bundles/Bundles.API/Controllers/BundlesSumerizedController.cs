using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Extensions;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.Summarized;

namespace OLM.Services.Bundles.API.Controllers
{
    [Route("api/bundles")]
    [ApiController]
    public class BundlesSumerizedController : ControllerBase
    {
        private readonly IBundlesSumRepository _bundleRepository;

        public BundlesSumerizedController(IBundlesSumRepository bundleRepository)
        {
            _bundleRepository = bundleRepository;
        }

        [HttpGet]
        [Route("daily")]
        public async Task<ActionResult<APIResponse<DailySummarizedBundlesAPIResponseViewModel>>> GetDailyBundles()
        {
            var data = await _bundleRepository.GetDailySumBundles();

            if (data != default && data.Count > 0)
            {
                var models = data.Select(b => b.ConvertToViewModel());

                return Ok(new APIResponse<DailySummarizedBundlesAPIResponseViewModel>(new DailySummarizedBundlesAPIResponseViewModel(models)));
            }
            else
            {
                return NotFound(new APIResponse<DailySummarizedBundlesAPIResponseViewModel>($"Egy géphez sincs feltöltve a mai naphoz rakatadat"));
            }
        }

        [HttpGet]
        [Route("weekly")]
        public async Task<ActionResult<APIResponse<WeeklySummarizedBundlesAPIResponseViewModel>>> GetWeeklyBundles()
        {
            var data = await _bundleRepository.GetWeeklySumBundles();

            if (data != default && data.Count > 0)
            {
                var models = data.Select(b => b.ConvertToViewModel());

                return Ok(new APIResponse<WeeklySummarizedBundlesAPIResponseViewModel>(new WeeklySummarizedBundlesAPIResponseViewModel(models)));
            }
            else
            {
                return NotFound(new APIResponse<WeeklySummarizedBundlesAPIResponseViewModel>($"Egy géphez sincs feltöltve rakatadat a héten"));
            }
        }
    }
}