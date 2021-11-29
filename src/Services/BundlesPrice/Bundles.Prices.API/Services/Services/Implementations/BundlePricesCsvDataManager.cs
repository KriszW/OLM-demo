using DocShow.Shared.Extensions.FluentValidationExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OLM.Services.Bundles.Prices.API.Data;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Services.Bundles.Prices.API.Services.Services.Abstractions;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocShow.Services.PDFConverter.API.Services.Services.Implementations
{
    public class BundlePricesCsvDataManager : IBundlePriceCsvDataManager
    {
        private readonly BundlePriceDbContext _dbContext;
        private readonly IValidator<BundlePriceModel> _validator;
        private readonly ICsvManager _csvManager;
        private readonly IExchangeRateRepository _exchangeRateRepository;


        public BundlePricesCsvDataManager(BundlePriceDbContext dbContext,
                                          ICsvManager csvManager,
                                          IValidator<BundlePriceModel> validator,
                                          IExchangeRateRepository exchangeRateRepository)
        {
            _dbContext = dbContext;
            _csvManager = csvManager;
            _validator = validator;
            _exchangeRateRepository = exchangeRateRepository;
        }

        public Task<byte[]> DownloadAsync()
        {
            return _csvManager.WriteAsync(_dbContext.BundlePrices);
        }

        public async Task UploadAsync(IFormFile file, string isoCode)
        {
            using var stream = file.OpenReadStream();

            var rates = await _exchangeRateRepository.GetRatesForCurrency(isoCode);

            await foreach (var item in _csvManager.ReadAsync<BundlePriceModel>(stream))
            {
                var validationResult = _validator.Validate(item);
                if (validationResult.IsValid == false) throw new APIErrorException(validationResult.ToAPIError());

                var rate = rates.Rates.FirstOrDefault(m => m.DestISOCode == item.Currency);
                if (rate == default) throw new APIErrorException($"A '{isoCode}'-hoz nincs feltöltve átváltási ráta '{item.Currency}'-ra");

                item.Currency = isoCode;
                item.Price *= rate.Rate;

                await _dbContext.BundlePrices.AddAsync(item);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UploadWithDeleteAllAsync(IFormFile file, string isoCode)
        {
            _dbContext.BundlePrices.RemoveRange(await _dbContext.BundlePrices.ToListAsync());

            await UploadAsync(file, isoCode);
        }
    }
}
