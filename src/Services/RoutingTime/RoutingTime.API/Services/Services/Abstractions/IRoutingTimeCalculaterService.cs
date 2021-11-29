using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Services.Services.Abstractions
{
    public interface IRoutingTimeCalculaterService
    {
        Task<List<RoutingTimesDataResponseViewModel>> Calculate(string machineName, DateTime start, DateTime end);
    }
}
