using Castle.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using OLM.Services.TCO.BackgroundTasks.Updater.Extensions;
using OLM.Services.TCO.BackgroundTasks.Updater.Models;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Factories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Repositories.Abstractions;
using OLM.Services.TCO.BackgroundTasks.Updater.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.TCO.BackgroundTasks.Tests.Services.Updater
{
    public class UpdateMethodTests
    {
        [Fact]
        public async Task Update_ShouldSuccess()
        {
            //Arrange
            var bundleID = "bundle1";
            var expectedDate = DateTime.Now;

            var expectedSourceModels = new List<TCODataSourceModel>()
            {
                new TCODataSourceModel(),
                new TCODataSourceModel(),
                new TCODataSourceModel()
            };
            var expectedDest = expectedSourceModels.Select(m => m.ConvertToDestinationModel());

            var mockedSourceRepo = new Mock<ISourceRepository>();
            mockedSourceRepo.Setup(sr => sr.GetDatetimeForBundle(bundleID)).ReturnsAsync(expectedDate);
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
            mockedSourceRepo.Verify(sr => sr.GetDatetimeForBundle(bundleID), Times.Never());
            mockedSourceRepo.Verify(sr => sr.GetModelsFrom(default), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleID(), Times.Never());
        }

        [Fact]
        public async Task UpdateFinishedDate_ShouldNotRunForLatestUpdatedState()
        {
            //Arrange
            var bundleID = "bundle100";
            var expectedDate = DateTime.Now;

            var expectedSourceModels = default(List<TCODataSourceModel>);

            var mockedSourceRepo = new Mock<ISourceRepository>();
            mockedSourceRepo.Setup(sr => sr.GetDatetimeForBundle(bundleID)).ReturnsAsync(expectedDate);
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
            mockedSourceRepo.Verify(sr => sr.GetDatetimeForBundle(bundleID), Times.Never());
            mockedSourceRepo.Verify(sr => sr.GetModelsFrom(expectedDate), Times.Once());
            mockedDestinationRepo.Verify(dr => dr.GetLatestBundleID(), Times.Never());
        }
    }
}
