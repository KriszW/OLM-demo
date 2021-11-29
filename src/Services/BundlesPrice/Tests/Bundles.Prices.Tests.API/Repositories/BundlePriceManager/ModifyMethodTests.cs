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
    public class ModifyMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 1;
            var newDimension = "46x6423";
            var newVendor = "13150";
            var model = new BundlePriceModel()
            {
                ID = id,
                RawMaterialItemNumber = newDimension,
                VendorID = newVendor
            };


            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newDimension, actual.RawMaterialItemNumber);
            Assert.Equal(newVendor, actual.VendorID);
        }

        [Fact]
        public async void Modify_ShouldSuccessForPriceChangeWithTheSameUnchangedDimension()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 1;
            var newPrice = 3M;
            var model = new BundlePriceModel()
            {
                ID = id,
                RawMaterialItemNumber = "5x25",
                Price = newPrice
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newPrice, actual.Price);
        }

        [Fact]
        public async void Modify_ShouldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 100;
            var model = new BundlePriceModel()
            {
                ID = id,
                RawMaterialItemNumber = "5x53225",
                Price = 56321
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(BundlePriceModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowAlreadyExistsDimensionForVendorID()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 1;
            var model = new BundlePriceModel()
            {
                ID = id,
                RawMaterialItemNumber = "51x25",
                VendorID = "10112423",
                Price = 3
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(BundlePriceModel.VendorID), actualException.ColumnName);
            Assert.Equal(model.VendorID, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowExceptionForChangingVendorIDWhichHasTheMatNumberAlready()
        {
            //Arrange
            var dbOptions = FakeBundlePriceDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeBundlePriceDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new BundlePriceDbContext(dbOptions);

            var id = 1;
            var model = new BundlePriceModel()
            {
                ID = id,
                RawMaterialItemNumber = "51x25",
                VendorID = "10112323",
                Price = 3
            };

            //Act
            var repo = new BundlePriceManagerRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(BundlePriceModel.VendorID), actualException.ColumnName);
            Assert.Equal(model.VendorID, actualException.ColumnValue);
        }
    }
}
