using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.Tram.API.Controllers;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using OLM.Shared.Models.Tram.SharedAPIModels.Request;
using OLM.Shared.Models.Tram.SharedAPIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Controllers.Trams
{
    public class FetchActionTests
    {
        [Fact]
        public async void ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramsRepository(dbContext);

            var model = new TramFetchRequestViewModel { Start = DateTime.Now.AddDays(-1), End = DateTime.Now.AddDays(2) };
            var expectedCount = 4;

            //Act
            var controller = new TramsController(repo);
            var actionResult = await controller.Fetch(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<IEnumerable<TramResponseViewModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<TramResponseViewModel>>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.Equal(expectedCount, actual.Model.Count());
        }
        [Fact]
        public async void ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramsRepository(dbContext);

            var model = new TramFetchRequestViewModel { Start = DateTime.Now.AddDays(10), End = DateTime.Now.AddDays(12) };

            var expectedMSG = $"A {model.Start} és {model.End} között nem található adat a rendszerben";

            //Act
            var controller = new TramsController(repo);
            var actionResult = await controller.Fetch(model);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<IEnumerable<TramResponseViewModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<TramResponseViewModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
