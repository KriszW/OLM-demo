using Fluxor;
using OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause
{
    public class InitPausesFeature : Feature<PausesPageState>
    {
        public override string GetName() => "PausesManagerCRUDPageInit";

        protected override PausesPageState GetInitialState()
            => new PausesPageState(0, 25);
    }
}
