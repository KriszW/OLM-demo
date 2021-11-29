using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using OLM.Services.DailyReport.API.Models;
using OLM.Services.DailyReport.API.Services.Services.Implementations;
using OLM.Services.DailyReport.API.ViewModels;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLM.Services.DailyReport.API.Tests.Services.DailyReportDataAggregation
{
    public class AggregateForDimensionMethodTests
    {
        [Fact]
        public async Task Aggregate_ShouldReturnOkay()
        {
            //Arrange
            var model = new ModelAggregatorViewModel 
            {
                Data = new List<DailyReportDataModel>
                {
                    new DailyReportDataModel
                    {
                        Dimension = "19x125",
                        Date = DateTime.Now,
                        LengthOfFS = 300.0,
                        Length = 25120.51,
                        LengthOfWaste = 6000.341,
                    },
                    new DailyReportDataModel
                    {
                        Dimension = "19x125",
                        Date = DateTime.Now,
                        LengthOfFS = 5100.0,
                        Length = 65120.51,
                        LengthOfWaste = 1000.341,
                    },
                },
                TargetModel = new TargetResponseViewModel
                {
                    Dimension = "19x125",
                    Intersection = 0.0011875,
                    Target = 0.1609,
                },
                TramModel = new DailyReportRequestTramViewModel
                {
                    Date = DateTime.Now,
                    Dimension = "19x125",
                    NumberOfLammela = 4,
                    NumberOfTram = 1
                }
            };

            var expectedTotalM3 = 107.16121125000001;
            var expectedWasteM3 = 8.313309875;
            var expectedFSM3 = 6.4125;
            var expectedTramM3 = 0.4;
            var expectedLamellaM3 = 0.00475;
            var expectedTotalWasteM3 = 15.130559875;
            var totalTargetM3 = 17.242238890125;

            //Act
            var service = new DailyReportDataAggregationProviderService();
            var actual = await service.AggregateForDimension(model);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedTotalM3, actual.TotalM3);
            Assert.Equal(expectedWasteM3, actual.WasteM3);
            Assert.Equal(expectedFSM3, actual.FSM3);
            Assert.Equal(expectedTramM3, actual.TramM3);
            Assert.Equal(expectedLamellaM3, actual.LammelaM3);
            Assert.Equal(expectedTotalWasteM3, actual.TotalWasteM3);
            Assert.Equal(totalTargetM3, actual.TargetWasteM3);
        }
    }
}
