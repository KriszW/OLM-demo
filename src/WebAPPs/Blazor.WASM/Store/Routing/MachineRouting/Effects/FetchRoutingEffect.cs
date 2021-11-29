using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing;
using OLM.Blazor.WASM.Store.Routing.MachineRouting.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting.Effects
{
    public class FetchRoutingEffect : Effect<StartFetchRoutingAction>
    {
        private readonly IRoutingRepository _routingRepository;
        private readonly ILogger<FetchRoutingEffect> _logger;

        public FetchRoutingEffect(IRoutingRepository routingRepository, ILogger<FetchRoutingEffect> logger)
        {
            _routingRepository = routingRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchRoutingAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingRepository.Fetch(action.MachineName);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchRoutingSucceededAction(action.MachineName, response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchRoutingFailedAction(action.MachineName, response.Message));
                }
            }
            catch (Exception ex)
            {
                var msg = $"'{action.MachineName}' nevű szabászsorhoz a routing lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchRoutingFailedAction(action.MachineName, msg));
            }
        }
    }
}
