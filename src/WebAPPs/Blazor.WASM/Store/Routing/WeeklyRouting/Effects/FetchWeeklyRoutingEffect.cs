using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing;
using OLM.Blazor.WASM.Store.Routing.MachineRouting.Effects;
using OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Effects
{
    public class FetchWeeklyRoutingEffect : Effect<StartFetchingWeeklyRoutingAction>
    {
        private readonly IRoutingRepository _routingRepository;
        private readonly ILogger<FetchRoutingEffect> _logger;

        public FetchWeeklyRoutingEffect(IRoutingRepository routingRepository, ILogger<FetchRoutingEffect> logger)
        {
            _routingRepository = routingRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchingWeeklyRoutingAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingRepository.FetchForWeek(action.MachineName);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchWeeklyRoutingSucceededAction(action.MachineName, response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchWeeklyRoutingFailedAction(action.MachineName, response.Message));
                }
            }
            catch (Exception ex)
            {
                var msg = $"'{action.MachineName}' nevű szabászsorhoz a heti routing lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchWeeklyRoutingFailedAction(action.MachineName, msg));
            }
        }
    }
}
