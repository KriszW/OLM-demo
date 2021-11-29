using OLM.Services.Routing.API.Data;
using OLM.Services.Routing.API.Models;
using OLM.Services.Routing.API.Services.Repositories.Implementations;
using OLM.Services.Routing.Tests.API.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Routing.Tests.API.Repositories.RoutingManager
{
    public class ModifyMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            var id = 1;
            var model = new RoutingModel()
            {
                ID = id,
            };


            //Act
            var repo = new RoutingManagerRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public async void Modify_houldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new RoutingDbContext(dbOptions);

            var id = 100;
            var model = new RoutingModel()
            {
                ID = id,
            };

            //Act
            var repo = new RoutingManagerRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(RoutingModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }
    }
}