using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Repositories.Trams
{
    public class FetchMethodTests
    {
        [Fact]
        public async Task Fetch_ShouldReturnData()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var model = new TramFetchRequestViewModel { Start = DateTime.Now.AddDays(-2), End = DateTime.Now.AddDays(1) };

            var expectedCount = 4;

            //Act
            var repo = new TramsRepository(dbContext);
            var actual = await repo.Fetch(model);

            //Assert
            Assert.Equal(expectedCount, actual.Count());
        }
    }
}
