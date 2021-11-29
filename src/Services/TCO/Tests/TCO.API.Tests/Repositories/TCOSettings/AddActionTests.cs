using Microsoft.EntityFrameworkCore;
using OLM.Services.TCO.API.Data;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Implementations;
using OLM.Services.TCO.API.Tests.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.TCO.API.Tests.Repositories.TCOSettings
{
    public class AddActionTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            var expectedID = (await dbContext.TCOConstansValues.MaxAsync(b => b.ID.GetValueOrDefault())) + 1;

            var model = new TCOValueSettingsModel()
            {
                ID = default,
                RawMaterialItemNumber = "5312x421",
                ExpectedTCOValue = 20.5,
                MaximumDifference = 0.4
            };

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            await repo.Add(model);
            var actual = await repo.GetByID(expectedID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID, actual.ID);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsID()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            var model = new TCOValueSettingsModel()
            {
                ID = 1,
                RawMaterialItemNumber = "5x425",
                ExpectedTCOValue = 20.5,
                MaximumDifference = 0.4
            };

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(TCOValueSettingsModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsDimension()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            var model = new TCOValueSettingsModel()
            {
                ID = default,
                RawMaterialItemNumber = "5x25",
                ExpectedTCOValue = 20.5,
                MaximumDifference = 0.4
            };

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(TCOValueSettingsModel.RawMaterialItemNumber), actualException.ColumnName);
            Assert.Equal(model.RawMaterialItemNumber, actualException.ColumnValue);
        }
    }
}
