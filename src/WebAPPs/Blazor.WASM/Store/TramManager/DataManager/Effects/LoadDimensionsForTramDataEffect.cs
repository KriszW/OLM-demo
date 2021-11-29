using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Blazor.WASM.Store.Manager.Tram.Dimensions.Effects;
using OLM.Blazor.WASM.Store.TramManager.DataManager.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager.Effects
{
    public class LoadDimensionsForTramDataEffect : Effect<LoadDimensionsForTramDataAction>
    {
        private readonly ITramDimensionRepository _tramDimensionRepository;
        private readonly ILogger<LoadDimensionsForTramDataEffect> _logger;

        public LoadDimensionsForTramDataEffect(ITramDimensionRepository tramDimensionRepository,
                                               ILogger<LoadDimensionsForTramDataEffect> logger)
        {
            _tramDimensionRepository = tramDimensionRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadDimensionsForTramDataAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _tramDimensionRepository.GetAllDimensions();

                if (response.Success)
                {
                    dispatcher.Dispatch(new DimensionForTramDataLoadSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new DimensionForTramDataLoadFailAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A dimenziók lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new DimensionForTramDataLoadFailAction(new APIError(msg)));
            }
        }
    }
}
