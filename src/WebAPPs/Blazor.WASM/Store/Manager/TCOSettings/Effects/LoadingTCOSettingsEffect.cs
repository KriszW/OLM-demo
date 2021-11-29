using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.TCO.TCOSettings;
using OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Effects
{
    public class LoadingTCOSettingsEffect : Effect<LoadTCOSettingsAction>
    {
        private readonly ITCOValueSettingsRepository _tcoValueSettingsRepo;
        private readonly ILogger<LoadingTCOSettingsEffect> _logger;

        public LoadingTCOSettingsEffect(ITCOValueSettingsRepository tcoValueSettingsRepo,
                                        ILogger<LoadingTCOSettingsEffect> logger)
        {
            _tcoValueSettingsRepo = tcoValueSettingsRepo;
            _logger = logger;
        }

        protected async override Task HandleAsync(LoadTCOSettingsAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _tcoValueSettingsRepo.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchSettingsModelSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchSettingsModelFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchSettingsModelFailedAction(new APIError(msg)));
            }
        }
    }
}
