using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Services.Services.Abstractions;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO;
using OLM.Shared.Models.TCO.SharedAPIModels.TCO.RawTCO;

namespace OLM.Services.TCO.API.Controllers
{
    [Route("api/tcoraw")]
    [ApiController]
    public class RawTCOCalculatorController : ControllerBase
    {
        private readonly IRawTCOAggregatorService _rawTCOAggregatorService;

        public RawTCOCalculatorController(IRawTCOAggregatorService rawTCOAggregatorService)
        {
            _rawTCOAggregatorService = rawTCOAggregatorService;
        }

        [HttpGet]
        [Route("single")]
        public async Task<ActionResult<APIResponse<BundleTCOAPIResponseViewModel>>> CalcForOne([FromQuery]RawTCOQueryDataViewModel model)
        {
            var tco = await _rawTCOAggregatorService.Aggregate(model);

            if (tco != default)
            {
                tco.CalculatedValue = Math.Round(tco.CalculatedValue, 2);
                return Ok(new APIResponse<BundleTCOAPIResponseViewModel>(tco));
            }
            else
            {
                return NotFound(new APIResponse<BundleTCOAPIResponseViewModel>($"A megadott cikk és beszállító kódhoz nem sikerült kiszámolni a TCO értékét"));
            }
        }

        [HttpGet]
        [Route("many")]
        public async Task<ActionResult<APIResponse<BundleTCOAPIResponseViewModel>>> CalcForMany([FromBody] IEnumerable<RawTCOQueryDataViewModel> models)
        {
            var tco = await _rawTCOAggregatorService.Aggregate(models);

            if (tco != default)
            {
                tco.CalculatedValue = Math.Round(tco.CalculatedValue, 2);
                return Ok(new APIResponse<BundleTCOAPIResponseViewModel>(tco));
            }
            else
            {
                return NotFound(new APIResponse<BundleTCOAPIResponseViewModel>("A megadott cikkekhez és beszállítókhoz nem sikerült kiszámolni a TCO értéket"));
            }
        }
    }
}
