using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.GateWay.SPA.SPAGtw.SharedModels.Machine;

namespace OLM.Blazor.WASM.Store.Shared.ToggleableSideBar
{
    public class ToggleableSideBarState
    {
        public bool Hidden { get; private set; }

        public ToggleableSideBarState(bool hidden)
        {
            Hidden = hidden;
        }
    }
}
