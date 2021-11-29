using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Reducers
{
    public class FetchWeeklyDayFollowUpSuccessReducer : Reducer<WeeklyDayFollowUpState, FetchWeeklyDayFollowUpSucceededAction>
    {
        public override WeeklyDayFollowUpState Reduce(WeeklyDayFollowUpState state, FetchWeeklyDayFollowUpSucceededAction action)
            => new WeeklyDayFollowUpState(state.Date, action.Data);
    }
}
