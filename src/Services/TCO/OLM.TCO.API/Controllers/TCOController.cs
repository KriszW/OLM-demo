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
using OLM.Services.TCO.API.Services.Services.Implementations;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;

namespace OLM.Services.TCO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TCOController : ControllerBase
    {
        private readonly ITCOCalculatorFromBundlesService _tcoService;

        public TCOController(ITCOCalculatorFromBundlesService tcoService)
        {
            _tcoService = tcoService;
        }

        [HttpGet]
        [Route("bundle")]
        public async Task<ActionResult<APIResponse<IEnumerable<TCODataAPIResponseViewModel>>>> GetTCOData([FromBody] IEnumerable<string> bundleIDs)
        {
            var data = await _tcoService.GetData(bundleIDs);

            if (data != default)
            {
                return Ok(new APIResponse<IEnumerable<TCODataAPIResponseViewModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<TCODataAPIResponseViewModel>>($"A rakatokhoz nem sikerült kiszámolni a TCO értékét"));
            }
        }


        [HttpGet]
        [Route("bundle/{bundleID}")]
        public async Task<ActionResult<APIResponse<double>>> CalculateTCO([FromRoute][CustomizeValidator(RuleSet = "BundleID")] string bundleID)
        {
            var tco = await _tcoService.CalculateTCO(bundleID);

            if (tco != EuroTCOCalculatorService.TCODefaultReturnValue)
            {
                return Ok(new APIResponse<double>(Math.Round(tco, 2)));
            }
            else
            {
                return NotFound(new APIResponse<double>($"A {bundleID} rakathoz nem sikerült kiszámolni a TCO értékét"));
            }
        }

        [HttpGet]
        [Route("bundles")]
        public async Task<ActionResult<APIResponse<double>>> CalculateAVGTCO([FromQuery][CustomizeValidator(RuleSet = "BundleIDs")] IEnumerable<string> bundleIDs)
        {
            var tco = await _tcoService.CalculateAVGTCO(bundleIDs);

            if (tco != EuroTCOCalculatorService.TCODefaultReturnValue)
            {
                return Ok(new APIResponse<double>(Math.Round(tco, 2)));
            }
            else
            {
                return NotFound(new APIResponse<double>("A rakatokhoz nem sikerült kiszámolni a TCO átlag értékét"));
            }
        }
    }
}