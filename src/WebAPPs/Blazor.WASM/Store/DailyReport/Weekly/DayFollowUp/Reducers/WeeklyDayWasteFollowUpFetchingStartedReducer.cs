using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp.Reducers
{
    public class WeeklyDayWasteFollowUpFetchingStartedReducer : Reducer<WeeklyDayFollowUpState, StartFetchingFollowUpData>
    {
        public override WeeklyDayFollowUpState Reduce(WeeklyDayFollowUpState state, StartFetchingFollowUpData action)
            => new WeeklyDayFollowUpState(action.Date);
    }
}
