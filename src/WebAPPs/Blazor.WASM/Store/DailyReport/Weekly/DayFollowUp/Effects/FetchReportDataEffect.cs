using Fluxor;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Weekly;
using OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Actions;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Effects
{
    public class FetchReportDataEffect : Effect<StartFetchingFollowUpData>
    {
        private readonly IWeeklyDayFollowUpRepository _weeklyDayFollowUpRepository;
        private readonly ILogger<FetchReportDataEffect> _logger;

        public FetchReportDataEffect(IWeeklyDayFollowUpRepository weeklyDayFollowUpRepository,
                                     ILogger<FetchReportDataEffect> logger)
        {
            _weeklyDayFollowUpRepository = weeklyDayFollowUpRepository;
            _logger = logger;
        }

        protected override async Task HandleAsync(StartFetchingFollowUpData action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _weeklyDayFollowUpRepository.FetchDayReport(action.Date);

                if (response.Success)
                {
                    dispatcher.Dispatch(new FetchWeeklyDayFollowUpSucceededAction(response.Model));
                }
                else
                {
                    dispatcher.Dispatch(new FetchWeeklyDayFollowUpFailedAction(response.Errors));
                }
            }
            catch (Exception ex)
            {
                var msg = $"A {action.Date} időhoz tartozó napi follow up selejt report lekérdezés közben váratlan hiba lépett fel";
                _logger.LogError(ex, msg);
                dispatcher.Dispatch(new FetchWeeklyDayFollowUpFailedAction(new APIError(msg)));
            }
        }
    }
}
