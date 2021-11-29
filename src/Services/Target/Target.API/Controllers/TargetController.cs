using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Abstractions;

namespace OLM.Services.Target.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly ITargetRepository _targetRepository;

        public TargetController(ITargetRepository targetRepository)
        {
            _targetRepository = targetRepository;
        }

        [HttpGet]
        [Route("dimension/{dimension}")]
        public async Task<ActionResult<APIResponse<WasteTargetDataModel>>> FetchDimension([FromRoute] string dimension)
        {
            var data = await _targetRepository.GetByDimension(dimension);

            if(data != default)
            {
                return Ok(new APIResponse<WasteTargetDataModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<WasteTargetDataModel>($"A '{dimension}' Selejt Target Dimenzióhoz nincs adat feltöltve az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("dimensions")]
        public async Task<ActionResult<APIResponse<IEnumerable<WasteTargetDataModel>>>> FetchDimension([FromQuery] IEnumerable<string> dims)
        {
            var data = await _targetRepository.GetByDimension(dims);

            if (data != default && data.Any() == true)
            {
                return Ok(new APIResponse<IEnumerable<WasteTargetDataModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<WasteTargetDataModel>>($"A megadott Selejt Target Dimenziókhoz nincs adat feltöltve az adatbázisba"));
            }
        }
    }
}
