namespace OLM.Blazor.WASM.Store.Shared.ToggleableSideBar.Actions
{
    public class ChangeMenuAction
    {
        public ChangeMenuAction(bool hidden)
        {
            Hidden = hidden;
        }

        public bool Hidden { get; private set; }
    }
}
