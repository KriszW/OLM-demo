using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OLM.Services.MoneyExchangeRate.API.Data;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations
{
    public class ExchangeRateCsvDataManager : ICSVDataManager<ExchangeRateCsvViewModel>
    {
        private readonly MoneyExchangeRatesDbContext _dbContext;
        private readonly ICsvManager _csvManager;

        public ExchangeRateCsvDataManager(MoneyExchangeRatesDbContext dbContext,
                                       ICsvManager csvManager)
        {
            _dbContext = dbContext;
            _csvManager = csvManager;
        }

        public Task<byte[]> DownloadAsync()
        {
            var data = _dbContext.Currencies.Include(m => m.Rates)
                                           .Select(m => new ExchangeRateCsvViewModel
                                           {
                                               Currency = m.ISOCode,
                                               Rates = m.Rates.Select(e => new RateCsvViewModel(e.DestISOCode, e.Rate)).ToList()
                                           });

            return _csvManager.WriteAsync(data);
        }

        public async Task UploadAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();

            await foreach (var item in _csvManager.ReadAsync<ExchangeRateCsvViewModel>(stream))
            {
                var codeTableModel = new CurrencyModel
                {
                    ID = default,
                    ISOCode = item.Currency,
                };

                var rates = item.Rates.Select(m => new ExchangeRateModel
                {
                    ID = default,
                    DestISOCode = m.ISOCode,
                    Rate = m.Rate
                });

                codeTableModel.Rates = rates.ToList();

                await _dbContext.Currencies.AddAsync(codeTableModel);
                await _dbContext.ExchangeRates.AddRangeAsync(rates);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UploadWithDeleteAllAsync(IFormFile file)
        {
            _dbContext.Currencies.RemoveRange(await _dbContext.Currencies.ToListAsync());
            _dbContext.ExchangeRates.RemoveRange(await _dbContext.ExchangeRates.ToListAsync());

            await UploadAsync(file);
        }
    }
}
