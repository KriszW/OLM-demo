using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;

namespace OLM.Services.TCO.API.Controllers
{
    [Route("api/bundle/tco/calc")]
    [ApiController]
    public class TCOCalculationForBundleController : ControllerBase
    {
        private IBundleTCOAggregatorService _bundleTCOAggregatorService;

        public TCOCalculationForBundleController(IBundleTCOAggregatorService bundleTCOAggregatorService)
        {
            _bundleTCOAggregatorService = bundleTCOAggregatorService;
        }

        [HttpGet]
        [Route("{bundleID}")]
        public async Task<ActionResult<APIResponse<BundleTCOAPIResponseViewModel>>> AggregateTCO([FromRoute][CustomizeValidator(RuleSet = "BundleID")] string bundleID)
        {
            var tco = await _bundleTCOAggregatorService.AggregateDataForBundle(bundleID);

            if (tco != default)
            {
                tco.CalculatedValue = Math.Round(tco.CalculatedValue, 2);
                return Ok(new APIResponse<BundleTCOAPIResponseViewModel>(tco));
            }
            else
            {
                return NotFound(new APIResponse<BundleTCOAPIResponseViewModel>($"A {bundleID} rakathoz nem sikerült kiszámolni a TCO értékét"));
            }
        }

        [HttpGet]
        [Route("/api/bundles/tco/calc")]
        public async Task<ActionResult<APIResponse<BundleTCOAPIResponseViewModel>>> AggregateAVGTCO([FromBody][CustomizeValidator(RuleSet = "BundleIDs")] IEnumerable<string> bundleIDs)
        {
            var tco = await _bundleTCOAggregatorService.AggregateDataForBundles(bundleIDs);

            if (tco != default)
            {
                tco.CalculatedValue = Math.Round(tco.CalculatedValue, 2);
                return Ok(new APIResponse<BundleTCOAPIResponseViewModel>(tco));
            }
            else
            {
                return NotFound(new APIResponse<BundleTCOAPIResponseViewModel>("A rakatokhoz nem sikerült kiszámolni a TCO átlag értékét"));
            }
        }
    }
}