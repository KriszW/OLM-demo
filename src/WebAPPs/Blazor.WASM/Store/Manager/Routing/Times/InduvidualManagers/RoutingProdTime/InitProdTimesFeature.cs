using Fluxor;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime
{
    public class InitProdTimesFeature : Feature<ProdTimesPageState>
    {
        public override string GetName() => "ProdTimeManagerCRUDPageInit";

        protected override ProdTimesPageState GetInitialState()
            => new ProdTimesPageState(0, 25);
    }
}
