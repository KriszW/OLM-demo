using CsvHelper;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping
{
    public class CsvManager : ICsvManager
    {
        private readonly ICsvReader _csvReader;
        private readonly ICsvWriter _csvWriter;

        public CsvManager(ICsvReader csvReader, ICsvWriter csvWriter)
        {
            _csvReader = csvReader;
            _csvWriter = csvWriter;
        }

        public IEnumerable<T> Read<T>(string path)
            where T : new()
            => _csvReader.Read<T>(path);

        public IEnumerable<T> Read<T>(Stream stream)
            where T : new()
            => _csvReader.Read<T>(stream);
        public IAsyncEnumerable<T> ReadAsync<T>(string path)
            where T : new()
            => _csvReader.ReadAsync<T>(path);
        public IAsyncEnumerable<T> ReadAsync<T>(Stream stream)
            where T : new()
            => _csvReader.ReadAsync<T>(stream);
        public void Write<T>(IEnumerable<T> data, string path)
            => _csvWriter.Write(data, path);

        public byte[] Write<T>(IEnumerable<T> data)
            => _csvWriter.Write(data);

        public Task WriteAsync<T>(IEnumerable<T> data, string path)
            => _csvWriter.WriteAsync(data, path);

        public Task<byte[]> WriteAsync<T>(IEnumerable<T> data)
            => _csvWriter.WriteAsync(data);
    }
}
