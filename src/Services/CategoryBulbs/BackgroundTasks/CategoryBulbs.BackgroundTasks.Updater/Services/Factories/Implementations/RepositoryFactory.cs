using Microsoft.Extensions.DependencyInjection;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Factories.Implementations
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IServiceProvider _serviceProvider;

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
