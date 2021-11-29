using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Services.Abstractions
{
    public interface ILivePagesService
    {
        Task Stop();

        Task Start();
    }
}
