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
    public class DeleteMethodTests
    {
        [Fact]
        public async void Delete_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new TramDbContext(dbOptions);

            var id = 1;

            //Act
            var repo = new TramDimensionRepository(dbContext);
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
            var dbContext = new TramDbContext(dbOptions);

            var id = 100;

            //Act
            var repo = new TramDimensionRepository(dbContext);
            var deleteTask = repo.Delete(id);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => deleteTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(TramDataModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }
    }
}
