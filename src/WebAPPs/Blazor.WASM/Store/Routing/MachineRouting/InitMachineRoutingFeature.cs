using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Routing.MachineRouting
{
    public class InitMachineRoutingFeature : Feature<MachineRoutingState>
    {
        public override string GetName() => "InitMachineRoutingPage";

        protected override MachineRoutingState GetInitialState() => new MachineRoutingState(default);
    }
}
