using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Effects
{
    public class LoadTramDimensionsEffect : Effect<LoadTramDimensionsAction>
    {
        private readonly ITramDimensionRepository _tramDimensionRepository;
        private readonly ILogger<LoadTramDimensionsEffect> _logger;

        public LoadTramDimensionsEffect(ITramDimensionRepository tramDimensionRepository,
                                        ILogger<LoadTramDimensionsEffect> logger)
        {
            _tramDimensionRepository = tramDimensionRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadTramDimensionsAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _tramDimensionRepository.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchTramDimensionsSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchTramDimensionsFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchTramDimensionsFailedAction(new APIError(msg)));
            }
        }
    }
}
