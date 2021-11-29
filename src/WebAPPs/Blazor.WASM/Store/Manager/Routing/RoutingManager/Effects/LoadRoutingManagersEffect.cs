using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager;

namespace OLM.Blazor.WASM.Store.Manager.Routing.RoutingManager.Effects
{
    public class LoadRoutingManagersEffect : Effect<LoadRoutingManagersAction>
    {
        private readonly IRoutingManagerRepository _routingManagerRepository;
        private readonly ILogger<LoadRoutingManagersEffect> _logger;

        public LoadRoutingManagersEffect(IRoutingManagerRepository routingManagerRepository,
                                         ILogger<LoadRoutingManagersEffect> logger)
        {
            _routingManagerRepository = routingManagerRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadRoutingManagersAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingManagerRepository.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchRoutingManagersSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchRoutingManagersFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchRoutingManagersFailedAction(new APIError(msg)));
            }
        }
    }
}
