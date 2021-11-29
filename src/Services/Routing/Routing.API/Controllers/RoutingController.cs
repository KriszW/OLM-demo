using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Routing.API.Services.Services.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Routing.SharedAPIModels.Request;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;

namespace OLM.Services.Routing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingController : ControllerBase
    {
        private readonly IRoutingService _routingService;

        public RoutingController(IRoutingService routingService)
        {
            _routingService = routingService;
        }

        [HttpGet]
        [Route("calculate")]
        public async Task<ActionResult<APIResponse<RoutingResponseViewModel>>> Calculate([FromQuery] RoutingRequestViewModel model)
        {
            var data = await _routingService.Calculate(model);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<RoutingResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<RoutingResponseViewModel>($"{model.Start}-tól {model.End}-ig nem számolható ki a Routing a '{model.MachineName}' szabászsorra"));
            }
        }
    }
}
