using Microsoft.EntityFrameworkCore;
using OLM.Services.RoutingTime.API.Data;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Implementations;
using OLM.Services.RoutingTime.Tests.API.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using Xunit;

namespace OLM.Services.RoutingTime.Tests.API.Repositories.ProductionTime
{
    public class AddMethodTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var expectedID = await dbContext.ProductionTimes.MaxAsync(b => b.ID.GetValueOrDefault()) + 1;

            var model = new ProductionTimeModel()
            {
                ID = default,
            };

            //Act
            var repo = new ProductionTimeRepository(dbContext);
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
            var dbContext = new RoutingTimeDbContext(dbOptions);

            var model = new ProductionTimeModel()
            {
                ID = 1,

            };

            //Act
            var repo = new ProductionTimeRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(ProductionTimeModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }
    }
}