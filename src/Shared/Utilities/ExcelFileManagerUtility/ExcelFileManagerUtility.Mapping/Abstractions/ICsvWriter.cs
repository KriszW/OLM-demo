using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions
{
    public interface ICsvWriter
    {
        void Write<T>(IEnumerable<T> data, string path);
        byte[] Write<T>(IEnumerable<T> data);
        Task WriteAsync<T>(IEnumerable<T> data, string path);
        Task<byte[]> WriteAsync<T>(IEnumerable<T> data);
    }
}
