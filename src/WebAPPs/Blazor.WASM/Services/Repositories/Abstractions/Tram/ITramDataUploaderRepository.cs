using OLM.Blazor.WASM.ViewModels.Tram;
using OLM.Services.SharedBases.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Tram
{
    public interface ITramDataUploaderRepository
    {
        Task<EmptyAPIResponse> Upload(TramDataEditViewModel model);
    }
}
