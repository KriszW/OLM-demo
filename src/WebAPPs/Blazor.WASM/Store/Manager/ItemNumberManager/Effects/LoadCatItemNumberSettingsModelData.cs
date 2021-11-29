using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.CategoryBulbs.Manager;
using OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager.Effects
{
    public class LoadCatItemNumberSettingsModelData : Effect<LoadCatBulbItemNumberAction>
    {
        private readonly ICategoryBulbsSettingsRepository _catBulbSettingsRepo;
        private readonly ILogger<LoadCatItemNumberSettingsModelData> _logger;

        public LoadCatItemNumberSettingsModelData(ICategoryBulbsSettingsRepository catBulbSettingsRepo,
                                                  ILogger<LoadCatItemNumberSettingsModelData> logger)
        {
            _catBulbSettingsRepo = catBulbSettingsRepo;
            _logger = logger;
        }

        protected async override Task HandleAsync(LoadCatBulbItemNumberAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _catBulbSettingsRepo.GetPaginatedData(action.PageIndex, action.PageSize);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchCatBulbItemNumberModelSuccessAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchCatBulbItemNumberModelFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab adattal lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchCatBulbItemNumberModelFailedAction(new APIError(msg)));
            }
        }
    }
}
