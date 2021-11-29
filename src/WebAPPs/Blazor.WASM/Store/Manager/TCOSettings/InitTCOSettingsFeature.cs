using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings
{
    public class InitTCOSettingsFeature : Feature<TCOSettingsManagerState>
    {
        public override string GetName() => "InitTCOSettingsManager";

        protected override TCOSettingsManagerState GetInitialState() => new TCOSettingsManagerState(0,25);
    }
}
