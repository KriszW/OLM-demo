using OLM.Services.Identity.API.Service.Repositories.Abstractions;
using OLM.Services.Identity.API.Service.Repositories.Implementations;
using OLM.Services.Identity.API.Service.Services.Abstractions;
using OLM.Services.Identity.API.Service.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Extensions
{
    public static class StartupServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => 
            services.AddSingleton<ISecretRepository, ConfigFileTokenSecretRepository>()
                .AddScoped<ITokenProviderService, JWTTokenProvider>()
                .AddScoped<IIdentityProviderService, IdentityProvider>();
    }
}
