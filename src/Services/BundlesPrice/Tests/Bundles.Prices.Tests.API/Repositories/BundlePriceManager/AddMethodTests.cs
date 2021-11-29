using Microsoft.EntityFrameworkCore;
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
    public class AddMethodTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var expectedID = (await dbContext.BundlePrices.MaxAsync(b => b.ID.GetValueOrDefault())) + 1;

            var model = new BundlePriceModel()
            {
                ID = default,
                RawMaterialItemNumber = "5312x421",
                Price = 5000,
                VendorID = "123"
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            await repo.Upload(model);
            var actual = await repo.GetByID(expectedID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID, actual.ID);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsID()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var model = new BundlePriceModel()
            {
                ID = 1,
                RawMaterialItemNumber = "5x425",
                Price = 50
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var uploadTask = repo.Upload(model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(BundlePriceModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsDimensionForVendor()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var model = new BundlePriceModel()
            {
                ID = default,
                RawMaterialItemNumber = "5x25",
                Price = 3,
                VendorID = "10112423"
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var uploadTask = repo.Upload(model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(BundlePriceModel.VendorID), actualException.ColumnName);
            Assert.Equal(model.VendorID, actualException.ColumnValue);
        }
    }
}
