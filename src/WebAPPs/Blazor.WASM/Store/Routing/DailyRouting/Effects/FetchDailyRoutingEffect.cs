using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing;
using OLM.Blazor.WASM.Store.Routing.DailyRouting.Actions;
using OLM.Blazor.WASM.Store.Routing.MachineRouting.Effects;
using OLM.Blazor.WASM.Store.Routing.WeeklyRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.DailyRouting.Effects
{
    public class FetchDailyRoutingEffect : Effect<StartFetchingDailyRoutingAction>
    {
        private readonly IRoutingRepository _routingRepository;
        private readonly ILogger<FetchDailyRoutingEffect> _logger;

        public FetchDailyRoutingEffect(IRoutingRepository routingRepository, ILogger<FetchDailyRoutingEffect> logger)
        {
            _routingRepository = routingRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchingDailyRoutingAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingRepository.FetchForDay(action.MachineName);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchDailyRoutingSucceededAction(action.MachineName, response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchDailyRoutingFailedAction(action.MachineName, response.Message));
                }
            }
            catch (Exception ex)
            {
                var msg = $"'{action.MachineName}' nevű szabászsorhoz a napi routing lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchDailyRoutingFailedAction(action.MachineName, msg));
            }
        }
    }
}
