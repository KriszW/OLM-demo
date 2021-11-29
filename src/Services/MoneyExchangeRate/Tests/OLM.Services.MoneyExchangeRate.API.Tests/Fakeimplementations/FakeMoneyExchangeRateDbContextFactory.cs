using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Tests.Fakeimplementations
{
    public static class FakeMoneyExchangeRateDbContextFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<MoneyExchangeRatesDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-olm-moneyexchangerate-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new MoneyExchangeRatesDbContext(options);

            await dbContext.AddRangeAsync(CreateModels());
            await dbContext.SaveChangesAsync();
        }

        public static List<CurrencyModel> CreateModels() => new List<CurrencyModel>()
        {
            new CurrencyModel
            {
                ISOCode = "HUF",
                Rates = new List<ExchangeRateModel>
                {
                    new ExchangeRateModel
                    {
                        DestISOCode = "EUR",
                        Rate = 0.0029M
                    },
                    new ExchangeRateModel
                    {
                        DestISOCode = "USD",
                        Rate = 0.0033M
                    }
                }
            },
            new CurrencyModel
            {
                ISOCode = "EUR",
                Rates = new List<ExchangeRateModel>
                {
                    new ExchangeRateModel
                    {
                        DestISOCode = "HUF",
                        Rate = 351.1M
                    },
                    new ExchangeRateModel
                    {
                        DestISOCode = "USD",
                        Rate = 1.13M
                    }
                }
            },
            new CurrencyModel
            {
                ISOCode = "USD",
                Rates = new List<ExchangeRateModel>
                {
                    new ExchangeRateModel
                    {
                        DestISOCode = "HUF",
                        Rate = 304.62M
                    },
                    new ExchangeRateModel
                    {
                        DestISOCode = "EUR",
                        Rate = 0.89M
                    }
                }
            },
        };
    }
}
