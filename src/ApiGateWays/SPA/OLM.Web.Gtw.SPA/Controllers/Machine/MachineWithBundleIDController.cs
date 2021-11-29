using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.BundleMachine.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Caching.Abstractions;
using OLM.Shared.Extensions.OneOfExtensions;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machines;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.Machine
{
    [Route("api/bundle/machine")]
    [ApiController]
    public class MachineWithBundleIDController : ControllerBase
    {
        private readonly IBundleMachineAggregator _bundleMachineAggregator;
        private readonly ICachingService<string, MachineViewModel> _machineCache;
        private readonly ICachingService<string, SummarizedMachineViewModel> _machinesCache;

        public MachineWithBundleIDController(IBundleMachineAggregator bundleMachineAggregator,
                                             ICachingService<string, MachineViewModel> machineCache = null,
                                             ICachingService<string, SummarizedMachineViewModel> machinesCache = null)
        {
            _bundleMachineAggregator = bundleMachineAggregator;
            _machineCache = machineCache;
            _machinesCache = machinesCache;
        }

        [HttpGet]
        [Route("{machineID}")]
        public async Task<ActionResult<APIResponse<MachineViewModel>>> GetDataForMachine([FromRoute] string machineID)
        {
            //var result = await _machineCache.TryGetValueSetAsync(machineID,
            //                                                     key => _bundleMachineAggregator.GetDataForMachine(key));
            var result = await _bundleMachineAggregator.GetDataForMachine(machineID);

            return Ok(result);
        }

        [HttpGet]
        [Route("/api/bundles/machines")]
        public async Task<ActionResult<APIResponse<SummarizedMachineViewModel>>> GetDataForMachines()
        {
            //var result = await _machinesCache.TryGetValueSetAsync("machines",
            //                                                      _ => _bundleMachineAggregator.GetDataForMachines());

            var result = await _bundleMachineAggregator.GetDataForMachines();

            return Ok(result);
        }
    }
}
