using OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingData.BackgroundTasks.Updater.Services.Factories.Abstractions
{
    public interface IRepositoryFactory
    {
        ISourceRepository CreateSource();
        IDestinationRepository CreateDestination();
    }
}
