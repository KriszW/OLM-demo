using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOBundle
{
    public class InitTCOBundleFeature : Feature<TCOBundleState>
    {
        public override string GetName() => "TCOBundleData";

        protected override TCOBundleState GetInitialState()
            => new TCOBundleState(DateTime.Now.AddDays(-7), DateTime.Now, 0, 25);
    }
}
