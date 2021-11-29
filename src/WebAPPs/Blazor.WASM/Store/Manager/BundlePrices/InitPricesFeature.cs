using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices
{
    public class InitPricesFeature : Feature<BundlePricesState>
    {
        public override string GetName() => "PricesCRUD";

        protected override BundlePricesState GetInitialState()
            => new BundlePricesState(0, 25);
    }
}
