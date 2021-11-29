using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.OneOfExtensions;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.Machine
{
    [Route("api/bundle/curr/machine")]
    [ApiController]
    public class MachineWithBundleIDAndMoneyExchangeController : ControllerBase
    {
        private readonly IBundleMachineAggregator _bundleMachineAggregator;

        public MachineWithBundleIDAndMoneyExchangeController(IBundleMachineAggregator bundleMachineAggregator)
        {
            _bundleMachineAggregator = bundleMachineAggregator;
        }

        [HttpGet]
        [Route("{machineID}")]
        public async Task<ActionResult<APIResponse<MachineViewModel>>> GetDataForMachine([FromRoute] string machineID, [FromQuery] string sourceCur = "DKK", [FromQuery] string destCur = "HUF")
        {
            var result = await _bundleMachineAggregator.GetDataForMachine(machineID);

            return result;
        }

        [HttpGet]
        [Route("/api/bundles/curr/machines")]
        public async Task<ActionResult<APIResponse<SummarizedMachineViewModel>>> GetDataForMachines([FromQuery] string sourceCur = "DKK", [FromQuery] string destCur = "HUF")
        {
            var result = await _bundleMachineAggregator.GetDataForMachines();

            return result;
        }
    }
}
