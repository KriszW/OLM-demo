using OLM.Services.Identity.API.ViewModels.IdentityProviderResults.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.ViewModels.IdentityProviderResults
{
    public class LogoutIdentityProviderResult : IdentityProviderResult
    {
        public LogoutIdentityProviderResult(bool success) : base(success)
        {
        }
    }
}
