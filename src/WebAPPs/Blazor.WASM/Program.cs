using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OLM.Blazor.WASM.Extensions;
using OLM.Blazor.WASM;

namespace OLM.Blazor.WASM
{
    public class Program
    {
        // TODO: Implement Redux with Blazor.Fluxer
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.AddAllServices();
            builder.RootComponents.Add<App>("app");


            await builder.Build().RunAsync();
        }
    }
}
