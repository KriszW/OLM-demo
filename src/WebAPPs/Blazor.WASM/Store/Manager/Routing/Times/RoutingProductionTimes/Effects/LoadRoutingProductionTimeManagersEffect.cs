using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Actions;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingProductionTimes.Effects
{
    public class LoadRoutingProductionTimeManagersEffect : Effect<LoadRoutingProductionTimeManagersAction>
    {
        private readonly IRoutingProductionTimeManagerRepository _routingProductionTimeManagerRepository;
        private readonly ILogger<LoadRoutingProductionTimeManagersEffect> _logger;

        public LoadRoutingProductionTimeManagersEffect(IRoutingProductionTimeManagerRepository routingProductionTimeManagerRepository,
                                                       ILogger<LoadRoutingProductionTimeManagersEffect> logger)
        {
            _routingProductionTimeManagerRepository = routingProductionTimeManagerRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadRoutingProductionTimeManagersAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingProductionTimeManagerRepository.GetDataPaginatedForWeeks(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchRoutingProductionTimeManagersSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchRoutingProductionTimeManagersFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchRoutingProductionTimeManagersFailedAction(new APIError(msg)));
            }
        }
    }
}
