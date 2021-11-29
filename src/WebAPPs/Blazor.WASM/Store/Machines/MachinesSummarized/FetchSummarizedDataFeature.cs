using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.MachinesSummarized
{
    public class FetchSummarizedDataFeature : Feature<MachineSummerizedState>
    {
        public override string GetName() => "FetchDataForSummarized";
        protected override MachineSummerizedState GetInitialState()
            => new MachineSummerizedState();
    }
}
