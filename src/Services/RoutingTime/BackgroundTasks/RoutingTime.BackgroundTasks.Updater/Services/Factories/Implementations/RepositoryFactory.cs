using Microsoft.Extensions.DependencyInjection;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Implementations
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDestinationRepository CreateDestination()
            => _serviceProvider.GetRequiredService<IDestinationRepository>();

        public IPausesRepository CreatePauses()
            => _serviceProvider.GetRequiredService<IPausesRepository>();

        public IProdTimeRepository CreateProdTime()
            => _serviceProvider.GetRequiredService<IProdTimeRepository>();

        public ISourceRepository CreateSource()
            => _serviceProvider.GetRequiredService<ISourceRepository>();
    }
}
