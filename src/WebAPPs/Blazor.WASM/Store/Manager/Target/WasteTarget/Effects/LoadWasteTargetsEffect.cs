using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Actions;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Target;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget.Effects
{
    public class LoadWasteTargetsEffect : Effect<LoadWasteTargetsAction>
    {
        private readonly IWasteTargetManagerRepository _wasteTargetManagerRepository;
        private readonly ILogger<LoadWasteTargetsEffect> _logger;

        public LoadWasteTargetsEffect(IWasteTargetManagerRepository wasteTargetManagerRepository,
                                      ILogger<LoadWasteTargetsEffect> logger)
        {
            _wasteTargetManagerRepository = wasteTargetManagerRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(LoadWasteTargetsAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _wasteTargetManagerRepository.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchWasteTargetsSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchWasteTargetsFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchWasteTargetsFailedAction(new APIError(msg)));
            }
        }
    }
}
