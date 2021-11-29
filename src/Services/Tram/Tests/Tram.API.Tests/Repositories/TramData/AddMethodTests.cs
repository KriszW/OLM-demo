using Microsoft.EntityFrameworkCore;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Services.Tram.API.Tests.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using Xunit;

namespace OLM.Services.Tram.API.Tests.Repositories.TramData
{
    public class AddMethodTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var expectedID = await dbContext.Trams.MaxAsync(b => b.ID.GetValueOrDefault()) + 1;

            var model = new TramDataModel()
            {
                ID = default,
                Dimension = new TramDimensionModel
                {
                    Dimension = "25x75"
                }
            };

            //Act
            var repo = new TramDataRepository(dbContext);
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
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var model = new TramDataModel()
            {
                ID = 1,

            };

            //Act
            var repo = new TramDataRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(TramDataModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }

        [Fact]
        public async void Add_ShouldThrowDimensionNotExist()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var model = new TramDataModel()
            {
                ID = default,
                Dimension = new TramDimensionModel
                {
                    Dimension = "12x21"
                }
            };

            //Act
            var repo = new TramDataRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            var exception = await Assert.ThrowsAsync<DataNotFoundWithSpecifiedColumnException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<DataNotFoundWithSpecifiedColumnException<string>>(exception);
            Assert.Equal(nameof(TramDataModel.Dimension.Dimension), actualException.ColumnName);
            Assert.Equal(model.Dimension.Dimension, actualException.ColumnValue);
        }
    }
}