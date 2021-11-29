using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.TramManager.DataManager
{
    public class InitTramDataManagerFeature : Feature<TramDataManagerState>
    {
        public override string GetName() => "TramDataUploaderManager";

        protected override TramDataManagerState GetInitialState() => new TramDataManagerState();
    }
}
