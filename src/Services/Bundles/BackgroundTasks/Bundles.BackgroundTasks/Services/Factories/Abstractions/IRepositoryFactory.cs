using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Abstractions
{
    public interface IRepositoryFactory
    {
        ISourceBundlesRepository CreateSourceRepo();
        ITCOSourceBundlesRepository CreateTCOSourceRepo();
        IDestinationBundlesRepository CreateDestinationRepo();
        ITCODestinationBundlesRepository CreateTCODestinationRepo();
    }
}
