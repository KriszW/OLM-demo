using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Effects
{
    public class LoadPausesEffect : Effect<LoadPausesAction>
    {
        private readonly IRoutingPauseManagerRepository _routingPauseManagerRepository;
        private readonly ILogger<LoadPausesEffect> _logger;

        public LoadPausesEffect(IRoutingPauseManagerRepository routingPauseManagerRepository,
                                ILogger<LoadPausesEffect> logger)
        {
            _routingPauseManagerRepository = routingPauseManagerRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadPausesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingPauseManagerRepository.GetPaginatedData(action.Year, action.WeekNumber, action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchPausesSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchPausesFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchPausesFailedAction(new APIError(msg)));
            }
        }
    }
}
