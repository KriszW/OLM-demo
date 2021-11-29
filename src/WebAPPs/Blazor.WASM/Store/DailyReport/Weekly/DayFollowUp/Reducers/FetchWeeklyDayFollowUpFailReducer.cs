using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Reducers
{
    public class FetchWeeklyDayFollowUpFailReducer : Reducer<WeeklyDayFollowUpState, FetchWeeklyDayFollowUpFailedAction>
    {
        public override WeeklyDayFollowUpState Reduce(WeeklyDayFollowUpState state, FetchWeeklyDayFollowUpFailedAction action)
            => new WeeklyDayFollowUpState(state.Date, action.Errors);
    }
}
