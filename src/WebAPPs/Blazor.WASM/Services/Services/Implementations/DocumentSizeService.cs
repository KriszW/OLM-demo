using Microsoft.JSInterop;
using OLM.Blazor.WASM.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Services.Implementations
{
    public class DocumentSizeService : IBrowserSizeService
    {
        private readonly IJSRuntime _jsRuntime;

        public DocumentSizeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<int> GetBrowserHeight()
            => await _jsRuntime.InvokeAsync<int>("getDocumentHeight", default);

        public async Task<int> GetBrowserWidth()
            => await _jsRuntime.InvokeAsync<int>("getDocumentWidth", default);
    }
}
