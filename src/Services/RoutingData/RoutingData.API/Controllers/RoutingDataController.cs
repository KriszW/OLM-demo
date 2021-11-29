using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.RoutingData.API.Services.Services.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Response;

namespace OLM.Services.RoutingData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingDataController : ControllerBase
    {
        private readonly IRoutingDataCalculatorService _routingDataCalculatorService;

        public RoutingDataController(IRoutingDataCalculatorService routingDataCalculatorService)
        {
            _routingDataCalculatorService = routingDataCalculatorService;
        }

        [HttpGet]
        [Route("calculate")]
        public async Task<ActionResult<APIResponse<RoutingDataResponseViewModel>>> CalculateRoutingData([FromQuery] FetchRoutingDataRequestViewModel model)
        {
            var data = await _routingDataCalculatorService.FetchDataForDimension(model);

            if (data != default && data.Any() == true)
            {
                return Ok(new APIResponse<RoutingDataResponseViewModel>(new RoutingDataResponseViewModel{ 
                    Start = model.Start,
                    End = model.End,
                    Data = data,
                }));
            }
            else
            {
                return NotFound(new APIResponse<RoutingDataResponseViewModel>($"{model.Start} és {model.End} között nem található routing méter adat a rendszerben a {model.MachineName} géphez"));
            }
        }
    }
}
