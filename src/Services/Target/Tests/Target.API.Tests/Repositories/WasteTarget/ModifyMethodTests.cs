using OLM.Services.Target.API.Data;
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Implementations;
using OLM.Services.Target.API.Tests.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Target.API.Tests.Repositories.WasteTarget
{
    public class ModifyMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            var id = 1;
            var newDimension = "46x6423";
            var model = new WasteTargetDataModel()
            {
                ID = id,
                Dimension = newDimension
            };


            //Act
            var repo = new WasteTargetRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newDimension, actual.Dimension);
        }

        [Fact]
        public async void Modify_ShouldSuccessForTargetChangeWithTheSameUnchangedDimension()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            var id = 1;
            var newTarget = 0.123;
            var model = new WasteTargetDataModel()
            {
                ID = id,
                Dimension = "25x75",
                Target = newTarget
            };

            //Act
            var repo = new WasteTargetRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newTarget, actual.Target);
        }

        [Fact]
        public async void Modify_ShouldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            var id = 100;
            var model = new WasteTargetDataModel()
            {
                ID = id,
                Dimension = "5x53225",
            };

            //Act
            var repo = new WasteTargetRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(WasteTargetDataModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowAlreadyExistsDimension()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TargetDbContext(dbOptions);

            var id = 1;
            var model = new WasteTargetDataModel()
            {
                ID = id,
                Dimension = "13x25",
            };

            //Act
            var repo = new WasteTargetRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(WasteTargetDataModel.Dimension), actualException.ColumnName);
            Assert.Equal(model.Dimension, actualException.ColumnValue);
        }
    }
}