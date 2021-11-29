using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Controllers;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Implementations;
using OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Controllers.MoneyExchangeRate
{
    public class GetPaginatedActionTests
    {
        [Theory]
        [InlineData(0, 30, 3)]
        [InlineData(1, 1, 1)]
        [InlineData(0, 2, 2)]
        public async void ShouldReturnSuccess(int pageIndex, int pageSize, int expecetedTotalCount)
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{pageIndex}-{pageSize}-{expecetedTotalCount}");
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            //Act
            var controller = new MoneyExchangeRateController(repo);
            var actionResult = await controller.Paginate(pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<CurrencyModel>>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<CurrencyModel>>>(requestObject.Value);
            Assert.True(actual.Success);
            Assert.NotNull(actual.Model);
            Assert.Equal(expecetedTotalCount, actual.Model.Data.Count());
        }

        [Theory]
        [MemberData(nameof(CreateNotFoundParameterData))]
        public async void ShouldReturnNotFound(int pageIndex, int pageSize, string expectedMSG)
        {
            //Arrange
            var dbOptions = FakeMoneyExchangeRateDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{pageIndex}-{pageSize}-{expectedMSG}");
            await FakeMoneyExchangeRateDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new MoneyExchangeRatesDbContext(dbOptions);
            var repo = new MoneyExchangeRateRepository(dbContext);

            //Act
            var controller = new MoneyExchangeRateController(repo);
            var actionResult = await controller.Paginate(pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);

            Assert.IsType<ActionResult<APIResponse<Paginated<CurrencyModel>>>>(actionResult);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Paginated<CurrencyModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateNotFoundParameterData() => new object[][]
        {
            new object[] { 30,30, "A megadott adatokkal nincs feltöltve valuta az adatbázisba" },
        };
    }
}
