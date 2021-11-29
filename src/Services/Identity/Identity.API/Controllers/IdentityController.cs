using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLM.Services.Identity.API.Models;
using OLM.Services.Identity.API.Service.Services.Abstractions;
using OLM.Services.Identity.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Identity.AccountAccessModels;
using OLM.Services.SharedBases.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OLM.Services.SharedBases.APIErrors;

namespace OLM.Services.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IIdentityProviderService _identityProviderService;

        public IdentityController(IIdentityProviderService identityProviderService)
        {
            _identityProviderService = identityProviderService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<APIResponse<string>>> Login([FromBody] LoginViewModel model)
        {
            var loginRes = await _identityProviderService.Login(model);

            if (loginRes.Success)
            {
                return Ok(new APIResponse<string>(model: loginRes.Token));
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(new APIResponse<string>("A bejelentkezés nem sikerült"));
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<APIResponse<string>>> Register([FromBody] RegisterNewUserViewModel model)
        {
            var registerResult = await _identityProviderService.Register(model);

            if (registerResult.Success)
            {
                return Ok(new APIResponse<string>(model: registerResult.Token));
            }
            else
            {
                var errors = new List<APIErrorItem>();

                foreach (var error in registerResult.RegisterResult.Errors)
                {
                    errors.Add(new APIErrorItem(error.Code, error.Description));
                }

                if (errors.Any() == false)
                {
                    errors.Add(new APIErrorItem("A regisztráció sikertelen volt"));
                }

                return BadRequest(new APIResponse<string>(new APIError(errors)));
            }
        }
    }
}