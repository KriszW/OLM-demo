using Fluxor;
using OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp.Reducers
{
    public class FetchingDimensionDataSuccessReducer : Reducer<DimensionDayWasteFollowUpState, FetchDimensionDataSucceededAction>
    {
        public override DimensionDayWasteFollowUpState Reduce(DimensionDayWasteFollowUpState state, FetchDimensionDataSucceededAction action)
            => new DimensionDayWasteFollowUpState(state.Date, action.Data);
    }
}
