using OLM.Services.Identity.API.Models;
using OLM.Services.Identity.API.Service.Services.Abstractions;
using OLM.Services.Identity.API.ViewModels;
using OLM.Services.Identity.API.ViewModels.IdentityProviderResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Shared.Models.Identity.AccountAccessModels;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Exceptions.APIResponse.Exceptions;

namespace OLM.Services.Identity.API.Service.Services.Implementations
{
    public class IdentityProvider : IIdentityProviderService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenProviderService _tokenProviderService;

        public IdentityProvider(UserManager<ApplicationUser> userManager,
                                ITokenProviderService tokenProviderService)
        {
            _userManager = userManager;
            _tokenProviderService = tokenProviderService;
        }

        public async Task<LoginIdentityProviderResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var result = await _userManager.CheckPasswordAsync(user, model.Password);


            if (result == true)
            {
                var token = _tokenProviderService.GenerateToken(user);

                return new LoginIdentityProviderResult(token, user, default);
            }
            else
            {
                
            }

            return new LoginIdentityProviderResult(default, default, default, false);
        }

        public async Task<RegisterIdentityProviderResult> Register(RegisterNewUserViewModel model)
        {
            var user = new ApplicationUser(model.UserName) 
            {
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var token = _tokenProviderService.GenerateToken(user);
                return new RegisterIdentityProviderResult(result, user, token, true);
            }
            else
            {
                var apiError = new APIError();
                apiError.AddRange(result.Errors.Select(m => new APIErrorItem(m.Description)));
                throw new APIErrorException(apiError);
            }
        }
    }
}
