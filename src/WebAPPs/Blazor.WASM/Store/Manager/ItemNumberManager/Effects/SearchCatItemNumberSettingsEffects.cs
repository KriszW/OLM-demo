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
    public class SearchCatItemNumberSettingsEffects : Effect<StartSearchCategoryItemNumberAction>
    {
        private readonly ICategoryBulbsSettingsRepository _catBulbSettingsRepo;
        private readonly ILogger<SearchCatItemNumberSettingsEffects> _logger;

        public SearchCatItemNumberSettingsEffects(ICategoryBulbsSettingsRepository catBulbSettingsRepo,
                                                  ILogger<SearchCatItemNumberSettingsEffects> logger)
        {
            _catBulbSettingsRepo = catBulbSettingsRepo;
            _logger = logger;
        }

        protected async override Task HandleAsync(StartSearchCategoryItemNumberAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _catBulbSettingsRepo.Search(action.CategoryQuery, action.PageIndex, action.PageSize);

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
                var msg = $"A {action.PageIndex} laphoz {action.PageSize} darab a {action.CategoryQuery} kereséshez adat lekérdezése közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchCatBulbItemNumberModelFailedAction(new APIError(msg)));
            }
        }
    }
}
