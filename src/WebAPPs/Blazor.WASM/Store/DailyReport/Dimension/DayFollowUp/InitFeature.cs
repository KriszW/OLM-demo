using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Dimension.DayFollowUp
{
    public class InitFeature : Feature<DimensionDayWasteFollowUpState>
    {
        public override string GetName() => "DailyDimensionWasteFollowUpReport";

        protected override DimensionDayWasteFollowUpState GetInitialState() => new DimensionDayWasteFollowUpState(DateTime.Now);
    }
}
