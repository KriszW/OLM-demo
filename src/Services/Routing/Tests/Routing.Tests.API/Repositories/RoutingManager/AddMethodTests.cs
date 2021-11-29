using Microsoft.EntityFrameworkCore;
using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Repositories.RoutingManager
{
    public class AddMethodTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            var expectedID = await dbContext.Routing.MaxAsync(b => b.ID.GetValueOrDefault()) + 1;

            var model = new RoutingModel()
            {
                ID = default,
               
            };

            //Act
            var repo = new RoutingManagerRepository(dbContext);
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
            var dbContext = new RoutingDbContext(dbOptions);

            var model = new RoutingModel()
            {
                ID = 1,

            };

            //Act
            var repo = new RoutingManagerRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(RoutingModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }
    }
}