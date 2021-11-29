using Microsoft.Extensions.Logging;
using Moq;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Tests.Services.Updater
{
    public class UpdateMethodTests
    {
        [Fact]
        public async Task Update_ShouldSuccess()
        {
            //Arrange
            var bundleID = "bundle1";
            var expectedDate = DateTime.Now;

            var expectedSourceModels = new List<SourceModel>()
            {
                new SourceModel(),
                new SourceModel(),
                new SourceModel()
            };

            var mockedSourceRepo = new Mock<ISourceRepository>();
            mockedSourceRepo.Setup(sr => sr.GetDateForBundleID(bundleID)).ReturnsAsync(expectedDate);
            mockedSourceRepo.Setup(sr => sr.GetModelsFrom(default)).ReturnsAsync(expectedSourceModels);

            var mockedDestinationRepo = new Mock<IDestinationRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleID()).ReturnsAsync(bundleID);

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestination()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSource()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>());
            await service.Update();

            //Assert
            mockedSourceRepo.Verify(sr => sr.GetDateForBundleID(bundleID), Times.Never());
            mockedSourceRepo.Verify(sr => sr.GetModelsFrom(default), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleID(), Times.Never());
        }

        [Fact]
        public async Task UpdateFinishedDate_ShouldNotRunForLatestUpdatedState()
        {
            //Arrange
            var bundleID = "bundle100";
            var expectedDate = DateTime.Now;

            var expectedSourceModels = default(List<SourceModel>);

            var mockedSourceRepo = new Mock<ISourceRepository>();
            mockedSourceRepo.Setup(sr => sr.GetDateForBundleID(bundleID)).ReturnsAsync(expectedDate);
            mockedSourceRepo.Setup(sr => sr.GetModelsFrom(expectedDate)).ReturnsAsync(expectedSourceModels);

            var mockedDestinationRepo = new Mock<IDestinationRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleID()).ReturnsAsync(bundleID);

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestination()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSource()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>())
            {
                LatestFinishedDate = expectedDate
            };

            await service.Update();

            //Assert
            mockedSourceRepo.Verify(sr => sr.GetDateForBundleID(bundleID), Times.Never());
            mockedSourceRepo.Verify(sr => sr.GetModelsFrom(expectedDate), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleID(), Times.Never());
        }
    }
}
