using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Weekly.DayFollowUp
{
    public class InitFeature : Feature<WeeklyDayFollowUpState>
    {
        public override string GetName() => "WeeklyDayWasteFollowUp";

        protected override WeeklyDayFollowUpState GetInitialState() => new WeeklyDayFollowUpState(DateTime.Now);
    }
}
