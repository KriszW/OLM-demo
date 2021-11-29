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
    public class MachinesSummarizedDataController : ControllerBase
    {
        private IMachineSumRepository _machineSumRepository;

        public MachinesSummarizedDataController(IMachineSumRepository machineSumRepository)
        {
            _machineSumRepository = machineSumRepository;
        }

        [HttpGet]
        [Route("/api/bundles/sumdata/daily")]
        public async Task<ActionResult<APIResponse<SummarizedDataForMachinesViewModel>>> GetDaily()
        {
            var data = await _machineSumRepository.GetAllDailyBundles();

            if (data != default)
            {
                return Ok(new APIResponse<SummarizedDataForMachinesViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<SummarizedDataForMachinesViewModel>($"Egy géphez sincs feltöltve a mai naphoz rakatadat"));
            }
        }

        [HttpGet]
        [Route("/api/bundles/sumdata/weekly")]
        public async Task<ActionResult<APIResponse<SummarizedDataForMachinesViewModel>>> GetWeekly()
        {
            var data = await _machineSumRepository.GetAllWeeklyBundles();

            if (data != default)
            {
                return Ok(new APIResponse<SummarizedDataForMachinesViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<SummarizedDataForMachinesViewModel>($"Egy géphez sincs feltöltve rakatadat a héten"));
            }
        }
    }
}