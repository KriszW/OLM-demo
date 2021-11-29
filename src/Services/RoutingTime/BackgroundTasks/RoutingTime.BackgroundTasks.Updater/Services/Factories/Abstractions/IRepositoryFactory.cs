using OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Services.Factories.Abstractions
{
    public interface IRepositoryFactory
    {
        ISourceRepository CreateSource();
        IDestinationRepository CreateDestination();

        IPausesRepository CreatePauses();
        IProdTimeRepository CreateProdTime();
    }
}
