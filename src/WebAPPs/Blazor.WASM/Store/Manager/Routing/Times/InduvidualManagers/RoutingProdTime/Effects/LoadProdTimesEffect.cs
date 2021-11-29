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
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Effects
{
    public class LoadProdTimesEffect : Effect<LoadProdTimesAction>
    {
        private readonly IRoutingProductionTimeManagerRepository _routingProductionTimeManagerRepository;
        private readonly ILogger<LoadProdTimesEffect> _logger;

        public LoadProdTimesEffect(IRoutingProductionTimeManagerRepository routingProductionTimeManagerRepository,
                                   ILogger<LoadProdTimesEffect> logger)
        {
            _routingProductionTimeManagerRepository = routingProductionTimeManagerRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadProdTimesAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _routingProductionTimeManagerRepository.GetPaginatedData(action.Year, action.WeekNumber, action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchProdTimesSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchProdTimesFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchProdTimesFailedAction(new APIError(msg)));
            }
        }
    }
}
