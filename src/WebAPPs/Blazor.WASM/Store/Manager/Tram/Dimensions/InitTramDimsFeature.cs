using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Tram.Dimensions
{
    public class InitTramDimsFeature : Feature<TramDimensionsState>
    {
        public override string GetName() => "TramDimensionsCRUD";

        protected override TramDimensionsState GetInitialState()
            => new TramDimensionsState(0, 25);
    }
}
