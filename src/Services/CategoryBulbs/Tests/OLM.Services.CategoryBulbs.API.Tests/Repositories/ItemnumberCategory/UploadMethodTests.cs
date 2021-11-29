using Microsoft.EntityFrameworkCore;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Models;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations;
using OLM.Services.CategoryBulbs.API.Tests.FakeImplementations;
using OLM.Shared.Exceptions.DataManagerExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OLM.Services.CategoryBulbs.API.Tests.Repositories.ItemnumberCategory
{
    public class UploadMethodTests
    {
        [Fact]
        public async void Add_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var expectedID = (await dbContext.Bundles.MaxAsync(b => b.ID.GetValueOrDefault())) + 1;

            var model = new ItemnumberCategoryModel()
            {
                ID = default,
                Itemnumber = "10642156MK",
                CategoryType = "3"
            };

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
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
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var model = new ItemnumberCategoryModel()
            {
                ID = 1,
                Itemnumber = "10113556MK",
                CategoryType = "3"
            };

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            var uploadTask = repo.Upload(model);

            //Assert
            var exception = await Assert.ThrowsAsync<PrimaryKeyAlreadyExistsException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<PrimaryKeyAlreadyExistsException<int>>(exception);
            Assert.Equal(nameof(ItemnumberCategoryModel.ID), actualException.ColumnName);
            Assert.Equal(model.ID, actualException.ColumnValue);
        }

        [Fact]
        public async void Add_ShouldThrowAlreadyExistsItemnumber()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var model = new ItemnumberCategoryModel()
            {
                ID = default,
                Itemnumber = "10113556MK",
                CategoryType = "3"
            };

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            var uploadTask = repo.Upload(model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(ItemnumberCategoryModel.Itemnumber), actualException.ColumnName);
            Assert.Equal(model.Itemnumber, actualException.ColumnValue);
        }
    }
}
