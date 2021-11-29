using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Reducers
{
    public class StartFetchingDimensionDataReducer : Reducer<DimensionDayWasteFollowUpState, StartFetchingDimensionDailyWasteFollowUpAction>
    {
        public override DimensionDayWasteFollowUpState Reduce(DimensionDayWasteFollowUpState state, StartFetchingDimensionDailyWasteFollowUpAction action)
            => new DimensionDayWasteFollowUpState(action.Date);
    }
}
