using OLM.Blazor.WASM.Services.Repositories.Abstractions.Account;
using OLM.Blazor.WASM.Utilities.APIGTWEndPoints;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Identity.AccountAccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Services.Repositories.Implementations.Account
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly HttpClient _httpClient;

        public IdentityProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<string>> Login(LoginViewModel model)
        {
            var res = await _httpClient.PostAsJsonAsync(APIGTWEndpoints.Identity.GetLogin(), model);

            return await res.Content.ReadFromJsonAsync<APIResponse<string>>();
        }

        public Task<string> Register(RegisterNewUserViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
