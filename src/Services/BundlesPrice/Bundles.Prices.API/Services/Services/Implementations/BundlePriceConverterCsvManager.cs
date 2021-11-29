using OLM.Services.Bundles.Prices.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using OLM.Services.Bundles.Prices.API.Maps;
using CsvHelper.Configuration;

namespace OLM.Services.Bundles.Prices.API.Services.Services
{
    public class BundlePriceConverterCsvManager : ICsvManager
    {
        public const string fieldDelimeter = ";";


        public IEnumerable<T> Read<T>(string path)
            where T : new()
            => throw new NotImplementedException();

        public IEnumerable<T> Read<T>(Stream stream)
            where T : new()
            => throw new NotImplementedException();

        public IAsyncEnumerable<T> ReadAsync<T>(string path)
            where T : new()
            => throw new NotImplementedException();
        public void Write<T>(IEnumerable<T> data, string path) => throw new NotImplementedException();

        public byte[] Write<T>(IEnumerable<T> data) => throw new NotImplementedException();

        public Task WriteAsync<T>(IEnumerable<T> data, string path) => throw new NotImplementedException();

        public IAsyncEnumerable<T> ReadAsync<T>(Stream stream)
            where T : new()
        {
            var reader = new StreamReader(stream);
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = fieldDelimeter,

            });

            csv.Context.RegisterClassMap<BundlePriceClassMap>();

            return csv.GetRecordsAsync<T>();
        }

        public async Task<byte[]> WriteAsync<T>(IEnumerable<T> data)
        {
            using var memoryStream = new MemoryStream();
            using var sw = new StreamWriter(memoryStream);

            await sw.WriteLineAsync(string.Join(fieldDelimeter, typeof(BundlePriceModel).GetProperties().Select(m => m.Name)));

            foreach (var item in data)
            {
                await sw.WriteLineAsync(item.ToString());
            }

            await sw.FlushAsync();
            return memoryStream.ToArray();
        }
    }
}
