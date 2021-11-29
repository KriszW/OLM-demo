using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions
{
    public interface ICsvReader
    {
        IEnumerable<T> Read<T>(string path)
            where T : new();

        IEnumerable<T> Read<T>(Stream stream)
            where T : new();

        IAsyncEnumerable<T> ReadAsync<T>(string path)
            where T : new();

        IAsyncEnumerable<T> ReadAsync<T>(Stream stream)
            where T : new();
    }
}
