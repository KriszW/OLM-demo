using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Reducers
{
    public class FetchYearlyWeeksFollowUpDataFailReducer : Reducer<YearlyWeeksFollowUpPageState, FetchYearlyWeeksFollowUpDataFailedAction>
    {
        public override YearlyWeeksFollowUpPageState Reduce(YearlyWeeksFollowUpPageState state, FetchYearlyWeeksFollowUpDataFailedAction action)
            => new YearlyWeeksFollowUpPageState(state.Start, state.End, action.Errors);
    }
}
