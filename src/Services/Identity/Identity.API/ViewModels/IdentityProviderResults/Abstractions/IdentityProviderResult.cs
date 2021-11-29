using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.ViewModels.IdentityProviderResults.Abstractions
{
    public class IdentityProviderResult 
    {
        public IdentityProviderResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
