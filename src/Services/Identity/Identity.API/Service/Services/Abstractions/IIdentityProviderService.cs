using OLM.Services.Identity.API.ViewModels;
using OLM.Services.Identity.API.ViewModels.IdentityProviderResults;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Service.Services.Abstractions
{
    public interface IIdentityProviderService
    {
        Task<LoginIdentityProviderResult> Login(LoginViewModel model);
        Task<RegisterIdentityProviderResult> Register(RegisterNewUserViewModel model);
    }
}
