using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Shared.ToggleableSideBar
{
    public class ToggleableSideBarFeature : Feature<ToggleableSideBarState>
    {
        public override string GetName() => "ToggleableSideBarMenu";

        protected override ToggleableSideBarState GetInitialState() => new ToggleableSideBarState(false);
    }
}
