using Microsoft.Extensions.Logging;
using Moq;
using OLM.Services.Bundles.BackgroundTasks.Updater.Models;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.Bundles.BackgroundTasks.Updater.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.Bundles.BackgroundTasks.Tests.Services.Services.Updater
{
    public class UpdateMethodTests
    {
        [Fact]
        public async Task Update_ShouldSuccess()
        {
            //Arrange
            var expectedDate = DateTime.Now;

            var expectedSourceModels = new List<BundleModel>()
            {
                new BundleModel(),
                new BundleModel(),
                new BundleModel()
            };

            var mockedSourceRepo = new Mock<ISourceBundlesRepository>();
            mockedSourceRepo.Setup(sr => sr.QueryBundlesFromDate(expectedDate)).ReturnsAsync(expectedSourceModels);

            var mockedDestinationRepo = new Mock<IDestinationBundlesRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleFinishedDate()).ReturnsAsync(expectedDate);
            mockedDestinationRepo.Setup(dr => dr.UploadBundles(expectedSourceModels));

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestinationRepo()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSourceRepo()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>())
            {
                LatestFinishedDate = expectedDate
            };
            await service.Update();

            //Assert
            mockedSourceRepo.Verify(sr => sr.QueryBundlesFromDate(expectedDate), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleFinishedDate(), Times.Never());
            mockedDestinationRepo.Verify(dr => dr.UploadBundles(expectedSourceModels), Times.Once());
        }

        [Fact]
        public async Task UpdateFinishedDate_ShouldNotRunForLatestUpdatedState()
        {
            //Arrange
            var expectedDate = DateTime.Now;

            var expectedSourceModels = default(List<BundleModel>);

            var mockedSourceRepo = new Mock<ISourceBundlesRepository>();
            mockedSourceRepo.Setup(sr => sr.QueryBundlesFromDate(expectedDate)).ReturnsAsync(expectedSourceModels);

            var mockedDestinationRepo = new Mock<IDestinationBundlesRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleFinishedDate()).ReturnsAsync(expectedDate);
            mockedDestinationRepo.Setup(dr => dr.UploadBundles(expectedSourceModels));

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestinationRepo()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSourceRepo()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>())
            {
                LatestFinishedDate = expectedDate
            };

            await service.Update();

            //Assert
            mockedSourceRepo.Verify(sr => sr.QueryBundlesFromDate(expectedDate), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleFinishedDate(), Times.Never());
            mockedDestinationRepo.Verify(dr => dr.UploadBundles(expectedSourceModels), Times.Never());
        }
    }
}
