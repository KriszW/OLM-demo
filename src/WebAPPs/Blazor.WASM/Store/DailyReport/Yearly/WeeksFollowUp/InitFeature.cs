using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Yearly.WeeksFollowUp
{
    public class InitFeature : Feature<YearlyWeeksFollowUpPageState>
    {
        public override string GetName() => "YearlyWeeksWasteFollowUp";

        protected override YearlyWeeksFollowUpPageState GetInitialState() => new YearlyWeeksFollowUpPageState(new DateTime(DateTime.Now.Year, 1, 1),DateTime.Now);
    }
}
