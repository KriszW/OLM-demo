using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.Tram
{
    public interface ITramService
    {
        Task<IEnumerable<TramResponseViewModel>> Fetch(TramFetchRequestViewModel model);
    }
}
