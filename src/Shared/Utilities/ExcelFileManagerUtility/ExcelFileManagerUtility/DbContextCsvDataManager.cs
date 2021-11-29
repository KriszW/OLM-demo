using CsvHelper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility
{
    public class DbContextCsvDataManager<TDbContext, TModel> : ICSVDataManager<TModel>
        where TDbContext : DbContext
        where TModel : class, new()
    {
        private readonly TDbContext _dbContext;
        private readonly ICsvManager _csvManager;
        private readonly IValidator<TModel> _validator;

        public DbContextCsvDataManager(TDbContext dbContext,
                                       ICsvManager csvManager,
                                       IValidator<TModel> validator = default)
        {
            _dbContext = dbContext;
            _csvManager = csvManager;
            _validator = validator;
        }

        public async Task UploadAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var dbSet = GetDbSet();
            var primaryKeyName = dbSet.EntityType.FindPrimaryKey().Properties[0].Name;
            var propType = typeof(TModel).GetProperty(primaryKeyName).PropertyType;
            var defaultPrimaryKeyValue = GetDefault(propType);

            try
            {
                await foreach (var item in _csvManager.ReadAsync<TModel>(stream))
                {
                    await ValidateAndThrowOnFailItemAsync(item);

                    item.GetType().GetProperty(primaryKeyName).SetValue(item, defaultPrimaryKeyValue);
                    await dbSet.AddAsync(item);
                }
            }
            catch (HeaderValidationException ex) when (ex.Message.ToLower().Contains("not found"))
            {
                throw new APIErrorException(new APIError(ex.InvalidHeaders.Select(m => new APIErrorItem(string.Join(" ", m.Names), ex.Message))));
            }

            await _dbContext.SaveChangesAsync();
        }

        private object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public async Task UploadWithDeleteAllAsync(IFormFile file)
        {
            GetDbSet().RemoveRange(await GetAllAsync());

            await UploadAsync(file);
        }

        public async Task<byte[]> DownloadAsync() => await _csvManager.WriteAsync(await GetAllAsync());

        private DbSet<TModel> GetDbSet()
        {
            return _dbContext.Set<TModel>();
        }

        private Task<List<TModel>> GetAllAsync() => GetDbSet().ToListAsync();

        private async Task ValidateAndThrowOnFailItemAsync(TModel item)
        {
            if (_validator != default)
            {
                var validationResult = await _validator.ValidateAsync(item);

                if (validationResult.IsValid == false)
                    throw new APIErrorException(new APIError(validationResult.Errors.Select(m => new APIErrorItem(m.ErrorCode, m.ErrorMessage))));
            }
        }
    }
}
