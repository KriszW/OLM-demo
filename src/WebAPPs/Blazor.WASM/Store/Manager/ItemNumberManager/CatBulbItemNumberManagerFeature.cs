using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.ItemNumberManager
{
    public class CatBulbItemNumberManagerFeature : Feature<CatBulbItemNumberManagerState>
    {
        public override string GetName() => "CatBulbItemNumberManager";

        protected override CatBulbItemNumberManagerState GetInitialState()
            => new CatBulbItemNumberManagerState(0,25);
    }
}
