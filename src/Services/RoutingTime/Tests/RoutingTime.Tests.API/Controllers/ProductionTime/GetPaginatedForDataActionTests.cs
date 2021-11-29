using Microsoft.AspNetCore.Mvc;
using OLM.Services.RoutingTime.API.Controllers;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Controllers.ProductionTime
{
    public class GetPaginatedForDataActionTests
    {
        [Theory]
        [InlineData(0, 30, 2)]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        public async void ShouldReturnSuccess(int pageIndex, int pageSize, int expecetedTotalCount)
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{pageIndex}-{pageSize}-{expecetedTotalCount}");
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);
            var repo = new ProductionTimeRepository(dbContext);

            var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            //Act
            var controller = new ProductionTimeController(repo);
            var actionResult = await controller.GetDataPaginated(weekNumber, pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<ProductionTimeModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<ProductionTimeModel>>>(requestObject.Value);
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
            var dbContext = new RoutingTimeDbContext(dbOptions);
            var repo = new ProductionTimeRepository(dbContext);

            var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            //Act
            var controller = new ProductionTimeController(repo);
            var actionResult = await controller.GetDataPaginated(weekNumber, pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<ProductionTimeModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<ProductionTimeModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateNotFoundParameterData() => new object[][]
        {
            new object[] { 30,30, "A megadott adatokkal nincs feltöltve nyitva tartási idő az adatbázisba" },
        };
    }
}
