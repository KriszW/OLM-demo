using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Services.Repositories.Abstractions
{
    public interface ITramsRepository
    {
        Task<IEnumerable<TramResponseViewModel>> Fetch(TramFetchRequestViewModel model);
    }
}
