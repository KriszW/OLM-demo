using Microsoft.Extensions.DependencyInjection;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Services.Factories.Implementations
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

        public ISourceRepository CreateSource()
            => _serviceProvider.GetRequiredService<ISourceRepository>();
    }
}
