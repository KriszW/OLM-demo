using OLM.Services.Identity.API.Models;
using OLM.Services.Identity.API.ViewModels.IdentityProviderResults.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.ViewModels.IdentityProviderResults
{
    public class RegisterIdentityProviderResult : IdentityProviderResult
    {
        public RegisterIdentityProviderResult(IdentityResult registerResult, ApplicationUser newUser, string token, bool success) : base(success)
        {
            RegisterResult = registerResult;
            NewUser = newUser;
            Token = token;
        }

        public IdentityResult RegisterResult { get; private set; }

        public ApplicationUser NewUser { get; private set; }

        public string Token { get; private set; }
    }
}
