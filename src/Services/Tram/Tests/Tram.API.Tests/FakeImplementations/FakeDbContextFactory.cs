using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Implementations;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Tram.API.Tests.FakeImplementations
{
    public static class FakeDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<TramDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-tram-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new TramDbContext(options);

            await dbContext.AddRangeAsync(CreateDims);
            await dbContext.AddRangeAsync(CreateModels);
            await dbContext.SaveChangesAsync();
        }

        public static List<TramDimensionModel> CreateDims = new List<TramDimensionModel>
        {
            new TramDimensionModel
            {
                ID = 1,
                Dimension = "25x75"
            },
            new TramDimensionModel
            {
                ID = 2,
                Dimension = "32x62"
            },
            new TramDimensionModel
            {
                ID = 3,
                Dimension = "25x150"
            },
        };

        public static List<TramDataModel> CreateModels = new List<TramDataModel>
        {
            new TramDataModel
            {
                ID = 1,
                Date = DateTime.Now,
                Dimension = CreateDims[0],
                NumberOfLamella = 1350,
                NumberOfTrams = 1,
                Shift = ShiftTypes.De,
                MachineID = "1",
            },
            new TramDataModel
            {
                 ID = 2,
                Date = DateTime.Now,
                Dimension = CreateDims[0],
                NumberOfLamella = 150,
                NumberOfTrams = 0,
                Shift = ShiftTypes.De,
                MachineID = "2",
            },
            new TramDataModel
            {
                ID = 3,
                Date = DateTime.Now.AddDays(-1),
                Dimension = CreateDims[1],
                NumberOfLamella = 1310,
                NumberOfTrams = 1,
                Shift = ShiftTypes.De,
                MachineID = "1",
            },
            new TramDataModel
            {
                ID = 4,
                Date = DateTime.Now.AddDays(-1),
                Dimension = CreateDims[2],
                NumberOfLamella = 1250,
                NumberOfTrams = 1,
                Shift = ShiftTypes.Du,
                MachineID = "1",
            },
        };
    }
}
