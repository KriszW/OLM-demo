using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OLM.Services.Tram.API.Data;
using OLM.Services.Tram.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations
{
    public class TramCsvDataManager : ICSVDataManager<TramDataModel>
    {
        private readonly TramDbContext _dbContext;
        private readonly ICsvManager _csvManager;

        public TramCsvDataManager(TramDbContext dbContext,
                                          ICsvManager csvManager)
        {
            _dbContext = dbContext;
            _csvManager = csvManager;
        }

        public Task<byte[]> DownloadAsync()
        {
            return _csvManager.WriteAsync(_dbContext.Trams.Select(m => $"{m.ID};{m.Date};{m.Shift};{m.Dimension.Dimension};{m.NumberOfLamella};{m.NumberOfTrams};{m.MachineID}"));
        }

        public async Task UploadAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var data = new List<TramDataModel>();
            await foreach (var item in _csvManager.ReadAsync<TramDataModel>(stream)) data.Add(item);

            var dimensions = await SaveDimensions(data);

            foreach (var item in data)
            {
                item.Dimension = dimensions.FirstOrDefault(m => m.Dimension == item.Dimension.Dimension);

                await _dbContext.Trams.AddAsync(item);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task<IEnumerable<TramDimensionModel>> SaveDimensions(List<TramDataModel> data)
        {
            var dimensions = data.Select(m => m.Dimension.Dimension).Distinct().Select(m => new TramDimensionModel { Dimension = m });
            await _dbContext.Dimensions.AddRangeAsync(dimensions);
            await _dbContext.SaveChangesAsync();

            var dataDims = data.Select(m => m.Dimension.Dimension);

            return _dbContext.Dimensions.Where(p => dataDims.Contains(p.Dimension));
        }

        public async Task UploadWithDeleteAllAsync(IFormFile file)
        {
            _dbContext.Trams.RemoveRange(await _dbContext.Trams.ToListAsync());
            _dbContext.Dimensions.RemoveRange(await _dbContext.Dimensions.ToListAsync());
            await _dbContext.SaveChangesAsync();

            await UploadAsync(file);
        }
    }
}
