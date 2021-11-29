using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.TCOBundle.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Bundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.Bundle
{
    [Route("")]
    public class TCOBundleController : Controller
    {
        private readonly ITCOBundleAggregator _tcoBundleAggregator;

        public TCOBundleController(ITCOBundleAggregator tcoBundleAggregator)
        {
            _tcoBundleAggregator = tcoBundleAggregator;
        }

        [HttpGet]
        [Route("api/tcobundle/")]
        public async Task<ActionResult<APIResponse<IEnumerable<TCOBundleModel>>>> Calculate([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            if (to == default)
            {
                to = new DateTime(from.Year, from.Month, from.Day, 23, 59, 59);
            }

            var data = await _tcoBundleAggregator.GetDataFrom(from, to);

            if (data != default)
            {
                return Ok(new APIResponse<IEnumerable<TCOBundleModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<TCOBundleModel>>($"A {from} dátumtól {to}-ig nincs elérhető több adat"));
            }
        }
    }
}
