using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Reducers
{
    public class FetchingDimensionDataFailReducer : Reducer<DimensionDayWasteFollowUpState, FetchDimensionDataFailedAction>
    {
        public override DimensionDayWasteFollowUpState Reduce(DimensionDayWasteFollowUpState state, FetchDimensionDataFailedAction action)
            => new DimensionDayWasteFollowUpState(state.Date, action.Errors);
    }
}
