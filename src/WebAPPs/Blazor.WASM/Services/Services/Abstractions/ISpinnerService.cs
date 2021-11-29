using System;

namespace OLM.Blazor.WASM.Services.Services.Abstractions
{
    public interface ISpinnerService
    {
        event Action OnHide;
        event Action OnShow;

        void Hide();
        void Show();
    }
}