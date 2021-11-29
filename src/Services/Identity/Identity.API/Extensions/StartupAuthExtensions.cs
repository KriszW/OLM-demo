using OLM.Services.Identity.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLM.Services.Identity.API.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OLM.Services.Identity.API.Extensions
{
    public static class StartupAuthExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<OLMIdentityDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Ha JWT tokent használok azonosításnak, akkor csak az IdentityCore servicet szabad hozzáadni
            // És nem szabad használni a SignInManager classt
            // Mert ha a SignInManager classt használom és a teljes AddIdentity servicet adom hozzá
            // Akkor az az összes olyan kérést ami Login nélkül készült és nincs Identity session cookie az nem fog műdködni
            // És hiába jó a JWT token, ha nincs bejelentkezve akkor mindig 401-es errort fog dobni
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<OLMIdentityDbContext>();

            return services;
        }
    }
}
