using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Yearly;
using OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Effects
{
    public class FetchYearlyWeeksWasteFollowUpDataEffect : Effect<StartFetchingYearlyWeeksFollowUpAction>
    {
        private readonly IYearlyWeeksFollowUpRepository _yearlyWeeksFollowUpRepository;
        private readonly ILogger<FetchYearlyWeeksWasteFollowUpDataEffect> _logger;

        public FetchYearlyWeeksWasteFollowUpDataEffect(IYearlyWeeksFollowUpRepository yearlyWeeksFollowUpRepository,
                                                       ILogger<FetchYearlyWeeksWasteFollowUpDataEffect> logger)
        {
            _yearlyWeeksFollowUpRepository = yearlyWeeksFollowUpRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchingYearlyWeeksFollowUpAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _yearlyWeeksFollowUpRepository.Fetch(action.Start, action.End);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchYearlyWeeksFollowUpDataSucceededAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchYearlyWeeksFollowUpDataFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.Start} időhoz tartozó éves heti összesítő follow up selejt report lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchYearlyWeeksFollowUpDataFailedAction(new APIError(msg)));
            }
        }
    }
}
