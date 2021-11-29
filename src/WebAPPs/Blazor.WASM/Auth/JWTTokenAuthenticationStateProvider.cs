using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using OLM.Blazor.WASM.Auth.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using OLM.Blazor.WASM.Services.Repositories.Abstractions.Storage;

namespace OLM.Blazor.WASM.Auth
{
    public class JWTTokenAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationService
    {
        public const string JwtTokenAccessName = "access_token";
        private const string JWTScheme = "Bearer";
        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        private IStorageRepository _tokenRepository;
        private HttpClient _httpClient;

        public JWTTokenAuthenticationStateProvider(IStorageRepository tokenRepository, HttpClient httpClient)
        {
            _tokenRepository = tokenRepository;
            _httpClient = httpClient;
        }
    

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtToken = await _tokenRepository.ReadToken<string>(JwtTokenAccessName);

            if (string.IsNullOrEmpty(jwtToken))
            {
                return Anonymous;
            }

            var authState = BuildAuthenticationState(jwtToken);

            var claims = authState.User.Claims;

            var exp = claims.FirstOrDefault(m => m.Type == "exp").Value;

            var time = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).ToLocalTime();

            if (DateTime.Now >= time)
            {
                await Logout();
                return Anonymous;
            }
                

            return authState;
        }

        public async Task Login(string token)
        {
            await _tokenRepository.SaveToken(JwtTokenAccessName, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            await _tokenRepository.DeleteToken(JwtTokenAccessName);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JWTScheme, token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
