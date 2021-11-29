﻿using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Repositories.TCOSettings
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(10, true)]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        public async void GetByID_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Equal(isNull, actual == default);
        }
    }
}
