using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Dimension;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Weekly;
using OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Effects
{
    public class FetchDimensionDataEffect : Effect<StartFetchingDimensionDailyWasteFollowUpAction>
    {
        private readonly IDimensionFollowUpRepository _dimensionFollowUpRepository;
        private readonly ILogger<FetchDimensionDataEffect> _logger;

        public FetchDimensionDataEffect(IDimensionFollowUpRepository dimensionFollowUpRepository,
                                        ILogger<FetchDimensionDataEffect> logger)
        {
            _dimensionFollowUpRepository = dimensionFollowUpRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchingDimensionDailyWasteFollowUpAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _dimensionFollowUpRepository.FetchWeeklyReport(action.Date);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchDimensionDataSucceededAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchDimensionDataFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.Date} időhoz tartozó napi follow up dimenzió report lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchDimensionDataFailedAction(new APIError(msg)));
            }
        }
    }
}
