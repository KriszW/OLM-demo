using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp.Reducers
{
    public class StartFetchingYearlyWeeksFollowUpReducer : Reducer<YearlyWeeksFollowUpPageState, StartFetchingYearlyWeeksFollowUpAction>
    {
        public override YearlyWeeksFollowUpPageState Reduce(YearlyWeeksFollowUpPageState state, StartFetchingYearlyWeeksFollowUpAction action)
            => new YearlyWeeksFollowUpPageState(action.Start, action.End);
    }
}
