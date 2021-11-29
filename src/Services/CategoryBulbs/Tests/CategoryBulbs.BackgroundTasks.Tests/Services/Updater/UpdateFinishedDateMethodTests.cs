using Microsoft.Extensions.Logging;
using Moq;
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
    public class UpdateFinishedDateMethodTests
    {
        [Fact]
        public async Task UpdateFinishedDate_ShouldSuccessAndBeNow()
        {
            //Arrange
            var now = DateTime.Now;
            var bundleID = "bundle1";
            var expectedDate = now;
            var mockedSourceRepo = new Mock<ISourceRepository>();
            mockedSourceRepo.Setup(sr => sr.GetDateForBundleID(bundleID)).ReturnsAsync(expectedDate);

            var mockedDestinationRepo = new Mock<IDestinationRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleID()).ReturnsAsync(bundleID);

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestination()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSource()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>());
            await service.UpdateFinishedDate();

            //Assert
            Assert.Equal(expectedDate, service.LatestFinishedDate, TimeSpan.FromSeconds(2));

            mockedSourceRepo.Verify(sr => sr.GetDateForBundleID(bundleID), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleID(), Times.Once());
        }

        [Fact]
        public async Task UpdateFinishedDate_ShouldReturnNullAndFinishedDateBe2WeeksBackFromNow()
        {
            //Arrange
            var bundleID = default(string);
            var expectedDate = DateTime.Now.AddDays(-8);
            var mockedSourceRepo = new Mock<ISourceRepository>();
            mockedSourceRepo.Setup(sr => sr.GetDateForBundleID(bundleID)).ReturnsAsync(new DateTime());

            var mockedDestinationRepo = new Mock<IDestinationRepository>();
            mockedDestinationRepo.Setup(dr => dr.GetLatestBundleID()).ReturnsAsync(bundleID);

            var mockedRepoFactory = new Mock<IRepositoryFactory>();
            mockedRepoFactory.Setup(m => m.CreateDestination()).Returns(mockedDestinationRepo.Object);
            mockedRepoFactory.Setup(m => m.CreateSource()).Returns(mockedSourceRepo.Object);

            //Act
            var service = new UpdaterService(mockedRepoFactory.Object, Mock.Of<ILogger<UpdaterService>>());
            await service.UpdateFinishedDate();

            //Assert
            Assert.Equal(expectedDate, service.LatestFinishedDate, TimeSpan.FromSeconds(2));

            mockedSourceRepo.Verify(sr => sr.GetDateForBundleID(bundleID), Times.Never());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleID(), Times.Once());
        }
    }
}
