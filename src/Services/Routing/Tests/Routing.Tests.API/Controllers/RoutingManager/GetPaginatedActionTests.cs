using Microsoft.AspNetCore.Mvc;
using OLM.Services.Routing.API.Controllers;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Controllers.RoutingManager
{
    public class GetPaginatedActionTests
    {
        [Theory]
        [InlineData(0, 30, 4)]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        public async void ShouldReturnSuccess(int pageIndex, int pageSize, int expecetedTotalCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{pageIndex}-{pageSize}-{expecetedTotalCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);
            var repo = new RoutingManagerRepository(dbContext);

            //Act
            var controller = new RoutingManagerController(repo);
            var actionResult = await controller.GetPaginated(pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<RoutingModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<RoutingModel>>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.Equal(expecetedTotalCount, actual.Model.Data.Count());
        }

        [Theory]
        [MemberData(nameof(CreateNotFoundParameterData))]
        public async void ShouldReturnNotFound(int pageIndex, int pageSize, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{pageIndex}-{pageSize}-{expectedMSG}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);
            var repo = new RoutingManagerRepository(dbContext);

            //Act
            var controller = new RoutingManagerController(repo);
            var actionResult = await controller.GetPaginated(pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<RoutingModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<RoutingModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateNotFoundParameterData() => new object[][]
        {
            new object[] { 30,30, "A megadott adatokkal nincs feltöltve routing idő az adatbázisba" },
        };
    }
}
