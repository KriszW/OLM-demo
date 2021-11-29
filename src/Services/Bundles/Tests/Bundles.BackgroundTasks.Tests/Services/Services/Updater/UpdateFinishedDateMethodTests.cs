using Microsoft.Extensions.Logging;
using Moq;
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
    public class UpdateFinishedDateMethodTests
    {
        [Fact]
        public async Task UpdateFinishedDate_ShouldSuccessAndBeNow()
        {
            //Arrange
            var now = DateTime.Now;
            var expectedDate = now;

            var mockedSourceRepo = new Mock<ISourceBundlesRepository>();

            var mockedDestinationRepo = new Mock<IDestinationBundlesRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleFinishedDate()).ReturnsAsync(expectedDate);

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestinationRepo()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSourceRepo()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>());
            await service.UpdateFinishedDate();

            //Assert
            Assert.Equal(expectedDate, service.LatestFinishedDate, TimeSpan.FromSeconds(2));

            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleFinishedDate(), Times.Once());
        }

        [Fact]
        public async Task UpdateFinishedDate_ShouldReturnNullAndFinishedDateBe2WeeksBackFromNow()
        {
            //Arrange
            var expectedDate = DateTime.Now.AddDays(-60);

            var mockedSourceRepo = new Mock<ISourceBundlesRepository>();

            var mockedDestinationRepo = new Mock<IDestinationBundlesRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleFinishedDate()).ReturnsAsync(default(DateTime));

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestinationRepo()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSourceRepo()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>());
            await service.UpdateFinishedDate();

            //Assert
            Assert.Equal(expectedDate, service.LatestFinishedDate, TimeSpan.FromSeconds(2));

            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleFinishedDate(), Times.Once());
        }
    }
}
