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
    public class ModifyActionTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            var id = 1;
            var newDimension = "46x6423";
            var model = new TCOValueSettingsModel()
            {
                ID = id,
                RawMaterialItemNumber = newDimension,
                ExpectedTCOValue = 20.5,
                MaximumDifference = 0.4
            };


            //Act
            var repo = new TCOSettingsRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newDimension, actual.RawMaterialItemNumber);
        }

        [Fact]
        public async void Modify_houldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            var id = 100;
            var model = new TCOValueSettingsModel()
            {
                ID = id,
                RawMaterialItemNumber = "5x53225",
                ExpectedTCOValue = 20.5,
                MaximumDifference = 0.4
            };

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(TCOValueSettingsModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowAlreadyExistsDimension()
        {
            //Arrange
            var dbOptions = FakeTCODbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeTCODbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TCODataDbContext(dbOptions);

            var id = 1;
            var model = new TCOValueSettingsModel()
            {
                ID = id,
                RawMaterialItemNumber = "13x25",
                ExpectedTCOValue = 20.5,
                MaximumDifference = 0.4
            };

            //Act
            var repo = new TCOSettingsRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(TCOValueSettingsModel.RawMaterialItemNumber), actualException.ColumnName);
            Assert.Equal(model.RawMaterialItemNumber, actualException.ColumnValue);
        }
    }
}
