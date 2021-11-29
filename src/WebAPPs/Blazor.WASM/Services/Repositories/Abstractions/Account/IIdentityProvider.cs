using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Abstractions.Account
{
    public interface IIdentityProvider
    {
        Task<APIResponse<string>> Login(LoginViewModel model);

        Task<string> Register(RegisterNewUserViewModel model);
    }
}
