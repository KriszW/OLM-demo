using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Machines.Machine
{
    public class FetchDataFeature : Feature<MachineState>
    {
        public override string GetName() => "FetchOneMachineData";

        protected override MachineState GetInitialState() => new MachineState();
    }
}
