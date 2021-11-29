using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Machine;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundles.APIResponses.SummarizedData;

namespace OLM.Services.Bundles.API.Controllers
{
    [ApiController]
    public class MachineSummarizedDataController : ControllerBase
    {
        private readonly IMachineBundlesRepository _machineBundlesRepository;

        public MachineSummarizedDataController(IMachineBundlesRepository machineBundlesRepository)
        {
            _machineBundlesRepository = machineBundlesRepository;
        }

        [HttpGet]
        [Route("api/bundles/sumdata/{machineName}/daily")]
        public async Task<ActionResult<APIResponse<SummarizedBundlesForMachineDataViewModel>>> GetDaily([FromRoute] string machineName)
        {
            var data = await _machineBundlesRepository.GetDailySummarizedData(machineName);

            if (data != default)
            {
                return Ok(new APIResponse<SummarizedBundlesForMachineDataViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<SummarizedBundlesForMachineDataViewModel>($"A '{machineName}' géphez nincs feltöltve a napi rakatok adatai"));
            }
        }

        [HttpGet]
        [Route("api/bundles/sumdata/{machineName}/weekly")]
        public async Task<ActionResult<APIResponse<SummarizedBundlesForMachineDataViewModel>>> GetWeekly([FromRoute] string machineName)
        {
            var data = await _machineBundlesRepository.GetWeeklySummarizedData(machineName);

            if (data != default)
            {
                return Ok(new APIResponse<SummarizedBundlesForMachineDataViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<SummarizedBundlesForMachineDataViewModel>($"A '{machineName}' géphez nincs feltöltve a heti rakatok adatai"));
            }
        }
    }
}