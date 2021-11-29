using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Repositories.TramDimensions
{
    public class ModifyMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 1;
            var newDimension = "21x12";
            var model = new TramDimensionModel()
            {
                ID = id,
                Dimension = newDimension
            };


            //Act
            var repo = new TramDimensionRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newDimension, actual.Dimension);
        }

        [Fact]
        public async void Modify_ShouldReturnSuccessForChangingDataWithTheSameDimension()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 1;
            var newDimension = "25x75";
            var model = new TramDimensionModel()
            {
                ID = id,
                Dimension = newDimension
            };


            //Act
            var repo = new TramDimensionRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newDimension, actual.Dimension);
        }

        [Fact]
        public async void Modify_ShouldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 100;
            var model = new TramDimensionModel()
            {
                ID = id,
            };

            //Act
            var repo = new TramDimensionRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(TramDimensionModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowDimensionAlreadyExists()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 1;
            var model = new TramDimensionModel()
            {
                ID = id,
                Dimension = "25x150"
            };

            //Act
            var repo = new TramDimensionRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(TramDimensionModel.Dimension), actualException.ColumnName);
            Assert.Equal(model.Dimension, actualException.ColumnValue);
        }
    }
}