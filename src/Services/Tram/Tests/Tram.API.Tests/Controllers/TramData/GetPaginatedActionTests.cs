using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.Tram.API.Controllers;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Controllers.TramData
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
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDataRepository(dbContext);

            //Act
            var controller = new TramDataController(repo);
            var actionResult = await controller.GetPaginated(pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<TramDataModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<TramDataModel>>>(requestObject.Value);
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
            var dbContext = new TramDbContext(dbOptions);
            var repo = new TramDataRepository(dbContext);

            //Act
            var controller = new TramDataController(repo);
            var actionResult = await controller.GetPaginated(pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<TramDataModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<TramDataModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateNotFoundParameterData() => new object[][]
        {
            new object[] { 30,30, "A megadott adatokkal nincs feltöltve csille adat az adatbázisba" },
        };
    }
}
