using Microsoft.AspNetCore.Http;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.Prices.API.Services.Services.Abstractions
{
    public interface IBundlePriceCsvDataManager
    {
        Task UploadAsync(IFormFile file, string isoCode);
        Task UploadWithDeleteAllAsync(IFormFile file, string isoCode);


        Task<byte[]> DownloadAsync();
    }
}
