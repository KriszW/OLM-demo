using OLM.Services.Identity.API.Models;
using OLM.Services.Identity.API.ViewModels.IdentityProviderResults.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.ViewModels.IdentityProviderResults
{
    public class LoginIdentityProviderResult : IdentityProviderResult
    {
        public LoginIdentityProviderResult(string token, ApplicationUser user, SignInResult result, bool success = true) : base(success)
        {
            Token = token;
            User = user;
            Result = result;
        }

        public string Token { get; private set; }
        public ApplicationUser User { get; private set; }
        public SignInResult Result { get; private set; }
    }
}
