using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Target.WasteTarget
{
    public class InitWasteTargetManagerFeature : Feature<WasteTargetManagerState>
    {
        public override string GetName() => "WasteTargetCRUD";

        protected override WasteTargetManagerState GetInitialState()
            => new WasteTargetManagerState(0, 25);
    }
}
