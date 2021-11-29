using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.DailyReport.Manager.DailyReportBackgroundDataUploader
{
    public class InitUploadFeature : Feature<DailyReportBackgroundDataUploaderState>
    { 
        public override string GetName()
            => "InitDailyReportDataUpdate";

        protected override DailyReportBackgroundDataUploaderState GetInitialState()
            => new DailyReportBackgroundDataUploaderState();
    }
}
