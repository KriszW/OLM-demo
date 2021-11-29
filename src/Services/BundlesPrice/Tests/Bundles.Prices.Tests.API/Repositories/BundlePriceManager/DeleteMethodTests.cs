using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Implementations;
using OLM.Services.Bundles.Prices.Tests.API.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.Bundles.Prices.Tests.API.Repositories.BundlePriceManager
{
    public class DeleteMethodTests
    {
        [Fact]
        public async void Delete_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 1;

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            await repo.Delete(id);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async void Delete_ShouldThrowNotFoundException()
        {
            //Arrange 
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 100;

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var deleteTask = repo.Delete(id);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => deleteTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(BundlePriceModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }
    }
}
