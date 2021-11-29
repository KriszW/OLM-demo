using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.Tram.API.Services.Repositories.Abstractions;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;

namespace OLM.Services.Tram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramsController : ControllerBase
    {

        private readonly ITramsRepository _tramsRepository;

        public TramsController(ITramsRepository tramsRepository)
        {
            _tramsRepository = tramsRepository;
        }

        [HttpGet]
        [Route("fetch")]
        public async Task<ActionResult<APIResponse<IEnumerable<TramResponseViewModel>>>> Fetch([FromQuery] TramFetchRequestViewModel model)
        {
            var data = await _tramsRepository.Fetch(model);

            if (data != default && data.Any() == true)
            {
                return Ok(new APIResponse<IEnumerable<TramResponseViewModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<TramResponseViewModel>>($"A {model.Start} és {model.End} között nem található adat a rendszerben"));
            }
        }
    }
}
