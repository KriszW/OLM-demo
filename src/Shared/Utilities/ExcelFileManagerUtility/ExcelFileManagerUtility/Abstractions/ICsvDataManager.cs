using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions
{
    public interface ICSVDataManager<TModel>
        where TModel : class
    {
        Task UploadAsync(IFormFile file);

        Task UploadWithDeleteAllAsync(IFormFile file);

        Task<byte[]> DownloadAsync();
    }
}
