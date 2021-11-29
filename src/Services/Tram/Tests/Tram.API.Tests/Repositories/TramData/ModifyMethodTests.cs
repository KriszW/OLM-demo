using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Repositories.TramData
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
            var newDimension = "25x150";
            var model = new TramDataModel()
            {
                ID = id,
                Dimension = new TramDimensionModel { Dimension = newDimension },
                Shift = ShiftTypes.Ej,
            };


            //Act
            var repo = new TramDataRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newDimension, actual.Dimension.Dimension);
        }

        [Fact]
        public async void Modify_houldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 100;
            var model = new TramDataModel()
            {
                ID = id,
            };

            //Act
            var repo = new TramDataRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(TramDataModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowDimensionNotExists()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 1;
            var model = new TramDataModel()
            {
                ID = id,
                Dimension = new TramDimensionModel { Dimension = "12x21" }
            };

            //Act
            var repo = new TramDataRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<DataNotFoundWithSpecifiedColumnException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<DataNotFoundWithSpecifiedColumnException<string>>(exception);
            Assert.Equal(nameof(TramDataModel.Dimension.Dimension), actualException.ColumnName);
            Assert.Equal(model.Dimension.Dimension, actualException.ColumnValue);
        }
    }
}