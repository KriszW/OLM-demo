using OLM.Services.Identity.API.Data;
using OLM.Services.Identity.API.Models;
using OLM.Services.Identity.API.Service.Repositories.Abstractions;
using OLM.Services.Identity.API.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Service.Services.Implementations
{
    public class JWTTokenProvider : ITokenProviderService
    {
        private const int ValidForNumberOfDays = 1;

        private OLMIdentityDbContext _dbContext;
        private ISecretRepository _secretRepository;

        public JWTTokenProvider(OLMIdentityDbContext dbContext, ISecretRepository secretRepository)
        {
            _dbContext = dbContext;
            _secretRepository = secretRepository;
        }

        public string GenerateToken(ApplicationUser user)
        {
            var claims = GetUserClaims(user);

            var token = new JwtSecurityToken(new JwtHeader(
                new SigningCredentials(
                     new SymmetricSecurityKey(_secretRepository.GetTokenSecretInBytes()),
                     SecurityAlgorithms.HmacSha256)),

                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> GetUserClaims(ApplicationUser user)
        {
            var output = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now).AddDays(ValidForNumberOfDays).ToUnixTimeSeconds().ToString()),
            };

            if (_dbContext.Roles != default && _dbContext.UserRoles != default)
            {
                var roles = from r in _dbContext.Roles
                            join ur in _dbContext.UserRoles on r.Id equals ur.RoleId
                            where ur.UserId == user.Id
                            select r;

                foreach (var role in roles)
                {
                    output.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            return output;
        }
    }
}
