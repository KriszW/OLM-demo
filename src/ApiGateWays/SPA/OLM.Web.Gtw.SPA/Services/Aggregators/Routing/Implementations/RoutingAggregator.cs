using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.Routing.Abstractions;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Routing;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.Routing.Implementations
{
    public class RoutingAggregator : IRoutingAggregator
    {
        private readonly IDailyRoutingService _dailyRoutingService;
        private readonly IWeeklyRoutingService _weeklyRoutingService;

        public RoutingAggregator(IDailyRoutingService dailyRoutingService, IWeeklyRoutingService weeklyRoutingService)
        {
            _dailyRoutingService = dailyRoutingService;
            _weeklyRoutingService = weeklyRoutingService;
        }
        
        public async Task<AggregatedRoutingViewModel> Aggregate(string machineID)
        {
            var dailyTask = _dailyRoutingService.FetchForDay(machineID);
            var weeklyTask = _weeklyRoutingService.FetchForWeek(machineID);

            await Task.WhenAll(dailyTask, weeklyTask);

            return new AggregatedRoutingViewModel
            {
                Daily = dailyTask.Result,
                Weekly = weeklyTask.Result,
            };
        }
    }
}
