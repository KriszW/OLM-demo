using OLM.Services.Identity.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Data
{
    public class OLMIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public OLMIdentityDbContext(DbContextOptions options) : base(options) { }
    }
}
