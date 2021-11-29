using OLM.Services.Identity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Service.Services.Abstractions
{
    public interface ITokenProviderService
    {
        string GenerateToken(ApplicationUser user);
    }
}
