using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.RoutingTime.API.Services.Services.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;

namespace OLM.Services.RoutingTime.API.Controllers
{
    [Route("api/routingtime")]
    [ApiController]
    public class RoutingTimeController : ControllerBase
    {
        private readonly IRoutingTimeCalculaterService _routingTimeCalculaterService;

        public RoutingTimeController(IRoutingTimeCalculaterService routingTimeCalculaterService)
        {
            _routingTimeCalculaterService = routingTimeCalculaterService;
        }
        
        [HttpGet]
        [Route("calculate")]
        public async Task<ActionResult<APIResponse<RoutingTimesResponseViewModel>>> Calculate([FromQuery] FetchRoutingRequestTimeViewModel model)
        {
            var data = await _routingTimeCalculaterService.Calculate(model.MachineName, model.Start, model.End);

            if (data != default && data.Any() == true)
            {
                return Ok(new APIResponse<RoutingTimesResponseViewModel>(new RoutingTimesResponseViewModel
                {
                    Data = data,
                    Start = model.Start,
                    End = model.End
                }));
            }
            else
            {
                return NotFound(new APIResponse<RoutingTimesResponseViewModel>($"{model.Start} és {model.End} között nem elérhető a routing idő a {model.MachineName} gépre"));
            }

        }
    }
}
