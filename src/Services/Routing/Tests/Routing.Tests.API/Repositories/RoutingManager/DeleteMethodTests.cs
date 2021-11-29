using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Repositories.RoutingManager
{
    public class DeleteMethodTests
    {
        [Fact]
        public async void Delete_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            var id = 1;

            //Act
            var repo = new RoutingManagerRepository(dbContext);
            await repo.Delete(id);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async void Delete_ShouldThrowNotFoundException()
        {
            //Arrange 
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            var id = 100;

            //Act
            var repo = new RoutingManagerRepository(dbContext);
            var deleteTask = repo.Delete(id);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => deleteTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(RoutingModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }
    }
}
