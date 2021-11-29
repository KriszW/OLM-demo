using OLM.Services.RoutingData.API.Models;
using OLM.Shared.Models.RoutingData.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingData.API.Services.Repositories.Abstractions
{
    public interface IRoutingDataRepository
    {
        Task<List<BundleDataModel>> FetchData(FetchRoutingDataRequestViewModel model);
    }
}
