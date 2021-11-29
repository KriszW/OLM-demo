using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.Routing.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Routing;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using OLM.Shared.Models.Routing.SharedAPIModels.Response;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.Routing
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingController : ControllerBase
    {
        private readonly IRoutingAggregator _routingAggregator;
        private readonly IRoutingService _routingService;
        private readonly IDailyRoutingService _dailyRoutingService;
        private readonly IWeeklyRoutingService _weeklyRoutingService;

        public RoutingController(IRoutingAggregator routingAggregator,
                                 IRoutingService routingService,
                                 IDailyRoutingService dailyRoutingService,
                                 IWeeklyRoutingService weeklyRoutingService)
        {
            _routingAggregator = routingAggregator;
            _routingService = routingService;
            _dailyRoutingService = dailyRoutingService;
            _weeklyRoutingService = weeklyRoutingService;
        }

        [HttpGet]
        [Route("{machineID}")]
        public async Task<ActionResult<APIResponse<AggregatedRoutingViewModel>>> Calculate([FromRoute] string machineID)
        {
            var data = await _routingAggregator.Aggregate(machineID);

            if (data != default)
            {
                return Ok(new APIResponse<AggregatedRoutingViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<AggregatedRoutingViewModel>($"A {machineID} szabászsorhoz nem lehet lekérni az adatokat"));
            }
        }

        [HttpGet]
        [Route("daily/{machineID}")]
        public async Task<ActionResult<APIResponse<RoutingResponseViewModel>>> CalculateForDay([FromRoute] string machineID)
        {
            var data = await _dailyRoutingService.FetchForDay(machineID);

            if (data != default)
            {
                return Ok(new APIResponse<RoutingResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<RoutingResponseViewModel>($"A {machineID} szabászsorhoz nem lehet lekérni az adatokat ehhez a naphoz"));
            }
        }

        [HttpGet]
        [Route("weekly/{machineID}")]
        public async Task<ActionResult<APIResponse<RoutingResponseViewModel>>> CalculateForWeek([FromRoute] string machineID)
        {
            var data = await _weeklyRoutingService.FetchForWeek(machineID);

            if (data != default)
            {
                return Ok(new APIResponse<RoutingResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<RoutingResponseViewModel>($"A {machineID} szabászsorhoz nem lehet lekérni az adatokat ehhez a héthez"));
            }
        }

        [HttpGet]
        [Route("calculate/{machineID}")]
        public async Task<ActionResult<APIResponse<RoutingResponseViewModel>>> CalculateForWeek([FromRoute] string machineID, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var data = await _routingService.Fetch(machineID, start, end);

            if (data != default)
            {
                return Ok(new APIResponse<RoutingResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<RoutingResponseViewModel>($"A {machineID} szabászsorhoz nem lehet lekérni az adatokat {start} és {end} között"));
            }
        }
    }
}
