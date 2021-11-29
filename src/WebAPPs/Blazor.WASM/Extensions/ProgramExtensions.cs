using Blazor.Extensions.Storage;
using OLM.Blazor.WASM.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using OLM.Blazor.WASM.Auth.Abstractions;
using OLM.Blazor.WASM.Services.Services.Abstractions;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Machine;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Machine;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Account;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Account;
using Microsoft.Extensions.Configuration;
using OLM.Blazor.WASM.Utilities.Settings;
using Fluxor;
using OLM.Blazor.WASM.Services.Services.Implementations;
using MatBlazor;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.TCO.TCOSettings;
using OLM.Blazor.WASM.Services.Repositories.Implementations.TCO.TCOSettings;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.CategoryBulbs.Manager;
using OLM.Blazor.WASM.Services.Repositories.Implementations.CategoryBulbs.Manager;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Storage;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Storage;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Weekly;
using OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Weekly;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Dimension;
using OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Dimension;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.DailyReport.Yearly;
using OLM.Blazor.WASM.Services.Repositories.Implementations.DailyReport.Yearly;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Tram;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Target;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Target;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.BundlePrices;
using OLM.Blazor.WASM.Services.Repositories.Implementations.BundlePrices;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.MoneyExchangeRate;
using OLM.Blazor.WASM.Services.Repositories.Implementations.MoneyExchangeRate;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Routing;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Routing.Manager;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Routing.Manager.Times;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Routing.Manager.Times;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Bundle;
using OLM.Blazor.WASM.Services.Repositories.Implementations.Bundle;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace OLM.Blazor.WASM.Extensions
{
    public static class ProgramExtensions
    {
        public static void AddAllServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(sp => new HttpClient() { BaseAddress = new Uri(sp.GetRequiredService<AppOptions>().APIurl) });
            builder.Services.AddServices();
            builder.Services.AddHotKeys();
            builder.Services.AddSettingsModels(builder.Configuration);
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomLeft;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 5000;
            });
        }

        public static IServiceCollection AddSettingsModels(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppOptions").Get<AppOptions>();
            var livePagesOptions = configuration.GetSection("LiveSettings").Get<LivePagesOptions>();

            if (appSettings == default)
            {
                throw new ApplicationException($"Az {nameof(appSettings)} beállítások null");
            }

            if (livePagesOptions == default)
            {
                throw new ApplicationException($"Az {nameof(livePagesOptions)} beállítások null");
            }

            return services
                   .AddSingleton(appSettings)
                   .AddSingleton(livePagesOptions);
        }

        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddSingleton<ISpinnerService, SpinnerService>()
                    .AddScoped<JWTTokenAuthenticationStateProvider>()
                    .AddScoped<AuthenticationStateProvider, JWTTokenAuthenticationStateProvider>(sp => sp.GetRequiredService<JWTTokenAuthenticationStateProvider>())
                    .AddScoped<IAuthenticationService, JWTTokenAuthenticationStateProvider>(sp => sp.GetRequiredService<JWTTokenAuthenticationStateProvider>())
                    .AddScoped<IStorageRepository, LocalStorageRepository>()
                    .AddScoped<IDimensionFollowUpRepository, HttpDimensionFollowUpRepository>()
                    .AddScoped<IWeeklyDayFollowUpRepository, HttpWeeklyDayWasteFollowUpService>()
                    .AddScoped<IYearlyWeeksFollowUpRepository, HttpFetchYearlyWeeksFollowUpRepository>()
                    .AddScoped<IMachineRepository, HttpMachineRepository>()
                    .AddScoped<IBundlePriceRepository, HttpBundlePriceRepository>()
                    .AddScoped<IRoutingRepository, HttpRoutingRepository>()
                    .AddScoped<IRoutingManagerRepository, HttpRoutingManagerRepository>()
                    .AddScoped<IRoutingPauseManagerRepository, HttpRoutingPauseManagerRepository>()
                    .AddScoped<IRoutingProductionTimeManagerRepository, HttpRoutingProductionTimeManagerRepository>()
                    .AddScoped<ITCOValueSettingsRepository, HttpTCOValueSettingsRepository>()
                    .AddScoped<ICategoryBulbsSettingsRepository, HttpCategoryBulbsSettingsRepository>()
                    .AddScoped<ITramDimensionRepository, HttpTramDimensionRepository>()
                    .AddScoped<ITramDataUploaderRepository, HttpTramDataUploaderRepository>()
                    .AddScoped<IWasteTargetManagerRepository, HttpWasteTargetManagerRepository>()
                    .AddScoped<ICurrencyRepository, HttpCurrencyRepository>()
                    .AddScoped<IExchangeRateRepository, HttpExchangeRateRepository>()
                    .AddScoped<IIdentityProvider, IdentityProvider>()
                    .AddScoped<ILivePagesService, LivePagesService>()
                    .AddScoped<IBrowserSizeService, DocumentSizeService>()
                    .AddScoped<IBundlePriceUploadRepository, HttpBundlePriceUploadRepository>()
                    .AddScoped<ITCOBundleRepository, TCOBundleRepository>()
                    .AddAuthorizationCore()
                    .AddStorage()
                    .AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly).UseReduxDevTools());
    }
}
