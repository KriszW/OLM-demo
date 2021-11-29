using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Data
{
    public class MoneyExchangeRatesDbContext : DbContext
    {
        public MoneyExchangeRatesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<CurrencyModel> Currencies { get; set; }

        public DbSet<ExchangeRateModel> ExchangeRates { get; set; }
    }
}
