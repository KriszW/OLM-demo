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
    public class UpdateMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var id = 1;
            var newItemnumber = "1014523TF";
            var newCategory = "2";
            var model = new ItemnumberCategoryModel()
            {
                ID = id,
                Itemnumber = newItemnumber,
                CategoryType = newCategory
            };


            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            await repo.Update(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newItemnumber, actual.Itemnumber);
        }

        [Fact]
        public async void Modify_ShouldSuccessForCategoryChangeWithTheSameUnchangedItemNumber()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var id = 1;
            var newCategory = "4";
            var model = new ItemnumberCategoryModel()
            {
                ID = id,
                Itemnumber = "10113556MK",
                CategoryType = newCategory
            };

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            await repo.Update(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newCategory, actual.CategoryType);
        }

        [Fact]
        public async void Modify_houldThrowIDNotFound()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var id = 100;
            var model = new ItemnumberCategoryModel()
            {
                ID = id,
                Itemnumber = "10113556MK",
                CategoryType = "3"
            };

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            var uploadTask = repo.Update(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundByValueException<int>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<NotFoundByValueException<int>>(exception);
            Assert.Equal(nameof(ItemnumberCategoryModel.ID), actualException.ColumnName);
            Assert.Equal(id, actualException.ColumnValue);
        }

        [Fact]
        public async void Modify_ShouldThrowAlreadyExistsItemNumber()
        {
            //Arrange
            var dbOptions = FakeCategoryBulbsDbContextFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCategoryBulbsDbContextFactory.InitDbContext(dbOptions);
            var dbContext = new CategoryBulbsDbContext(dbOptions);

            var id = 1;
            var model = new ItemnumberCategoryModel()
            {
                ID = id,
                Itemnumber = "10113500AD"
            };

            //Act
            var repo = new ItemnumberCategoryRepository(dbContext);
            var uploadTask = repo.Update(id, model);

            //Assert
            var exception = await Assert.ThrowsAsync<UniqueDataAlreadyExistsException<string>>(() => uploadTask);
            var actualException = Assert.IsAssignableFrom<UniqueDataAlreadyExistsException<string>>(exception);
            Assert.Equal(nameof(ItemnumberCategoryModel.Itemnumber), actualException.ColumnName);
            Assert.Equal(model.Itemnumber, actualException.ColumnValue);
        }
    }
}
