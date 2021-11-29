using Microsoft.Extensions.DependencyInjection;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Implementations
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDestinationBundlesRepository CreateDestinationRepo() 
            => _serviceProvider.GetRequiredService<IDestinationBundlesRepository>();

        public ISourceBundlesRepository CreateSourceRepo()
            => _serviceProvider.GetRequiredService<ISourceBundlesRepository>();

        public ITCODestinationBundlesRepository CreateTCODestinationRepo()
            => _serviceProvider.GetRequiredService<ITCODestinationBundlesRepository>();

        public ITCOSourceBundlesRepository CreateTCOSourceRepo()
            => _serviceProvider.GetRequiredService<ITCOSourceBundlesRepository>();
    }
}
