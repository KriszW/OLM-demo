using Microsoft.EntityFrameworkCore;
using OLM.Services.Identity.API.Data;
using OLM.Services.Identity.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Tests.FakeImplementations
{
    public static class FakeIdentityDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<OLMIdentityDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-identity-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new OLMIdentityDbContext(options);

            await dbContext.AddRangeAsync(CreateUsers());
            await dbContext.SaveChangesAsync();
        }

        public static List<ApplicationUser> CreateUsers() => new List<ApplicationUser>()
        {

        };
    }
}
