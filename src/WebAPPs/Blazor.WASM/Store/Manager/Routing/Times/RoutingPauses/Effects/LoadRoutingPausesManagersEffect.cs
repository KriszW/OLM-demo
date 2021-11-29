using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.RoutingPauses.Effects
{
    public class LoadRoutingPausesManagersEffect : Effect<LoadRoutingPausesManagersAction>
    {
        private readonly IRoutingPauseManagerRepository _routingPauseManagerRepository;
        private readonly ILogger<LoadRoutingPausesManagersEffect> _logger;

        public LoadRoutingPausesManagersEffect(IRoutingPauseManagerRepository routingPauseManagerRepository,
                                               ILogger<LoadRoutingPausesManagersEffect> logger)
        {
            _routingPauseManagerRepository = routingPauseManagerRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadRoutingPausesManagersAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingPauseManagerRepository.GetDataPaginatedForWeeks(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchRoutingPausesManagersSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchRoutingPausesManagersFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchRoutingPausesManagersFailedAction(new APIError(msg)));
            }
        }
    }
}
