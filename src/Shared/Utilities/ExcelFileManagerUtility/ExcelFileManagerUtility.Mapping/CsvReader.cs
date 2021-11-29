using CsvHelper.Configuration;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping
{
    public class CsvReader : ICsvReader
    {
        private static CsvConfiguration csvConfiguration => new(CultureInfo.CurrentCulture)
        {
            Delimiter = ";",
        };

        public IEnumerable<T> Read<T>(string path)
            where T : new()
        {
            var csvHelper = CreateCsvReader(path);

            return csvHelper.GetRecords<T>();
        }

        public IEnumerable<T> Read<T>(Stream stream)
            where T : new()
        {
            var csvHelper = CreateCsvReader(stream);

            return csvHelper.GetRecords<T>();
        }

        public IAsyncEnumerable<T> ReadAsync<T>(string path)
            where T : new()
        {
            var csvHelper = CreateCsvReader(path);

            return csvHelper.GetRecordsAsync<T>();
        }

        public IAsyncEnumerable<T> ReadAsync<T>(Stream stream)
            where T : new()
        {
            var csvHelper = CreateCsvReader(stream);

            return csvHelper.GetRecordsAsync<T>();
        }

        private static CsvHelper.CsvReader CreateCsvReader(Stream stream)
        {
            return new CsvHelper.CsvReader(new StreamReader(stream), csvConfiguration);
        }

        private static CsvHelper.CsvReader CreateCsvReader(string path)
        {
            using var sr = new StreamReader(path);
            return new CsvHelper.CsvReader(sr, csvConfiguration);
        }
    }
}
