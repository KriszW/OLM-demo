using Fluxor;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Storage;
using OLM.Blazor.WASM.Store.Shared.ToggleableSideBar.Actions;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Shared.ToggleableSideBar.Effects
{
    public class MenuChangedEffect : Effect<ChangeMenuAction>
    {
        private readonly IStorageRepository _storageRepository;

        public MenuChangedEffect(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        protected override async Task HandleAsync(ChangeMenuAction action, IDispatcher dispatcher)
        {
            await _storageRepository.SaveToken("hiddenMenu", action.Hidden);

            dispatcher.Dispatch(action);
        }
    }
}
