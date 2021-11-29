using CsvHelper.Configuration;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping
{
    public class CsvWriter : ICsvWriter
    {
        private static CsvConfiguration csvConfiguration => new(CultureInfo.CurrentCulture)
        {
            Delimiter = ";",
        };

        public void Write<T>(IEnumerable<T> data, string path)
        {
            using var sw = new StreamWriter(path);
            using var csvHelper = new CsvHelper.CsvWriter(sw, csvConfiguration);

            csvHelper.WriteRecords(data);

            sw.Flush();
        }

        public async Task WriteAsync<T>(IEnumerable<T> data, string path)
        {
            using var sw = new StreamWriter(path);
            using var csvHelper = new CsvHelper.CsvWriter(sw, csvConfiguration);

            await csvHelper.WriteRecordsAsync(data);

            await sw.FlushAsync();
        }

        public byte[] Write<T>(IEnumerable<T> data)
        {
            using var memoryStream = new MemoryStream();
            using var sw = new StreamWriter(memoryStream);
            var csvHelper = new CsvHelper.CsvWriter(sw, csvConfiguration);

            csvHelper.WriteRecords(data);

            sw.Flush();

            return memoryStream.ToArray();
        }

        public async Task<byte[]> WriteAsync<T>(IEnumerable<T> data)
        {
            using var memoryStream = new MemoryStream();
            using var sw = new StreamWriter(memoryStream);
            var csvHelper = new CsvHelper.CsvWriter(sw, csvConfiguration);

            await csvHelper.WriteRecordsAsync(data);

            await sw.FlushAsync();

            return memoryStream.ToArray();
        }
    }
}
